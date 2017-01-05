using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extant.Pubmed.EBIPublicationsService;
using EBIClient = Extant.Pubmed.EBIPublicationsService.WSCitationImplClient;


namespace Extant.Pubmed
{
    public class EBIPubmedService : IPubmedService
    {
        private EBIClient client;

        // due to differences in page handling between PubmedService and the EBI SOAP service result cacheing is required across a session.
        // When using the EBIPubmedService, an instance of the service must be maintained in the httpsession.

        // cache to hold results
        private Dictionary<string, EBIPubMedDetailedResult> cache;

        // maintenance variabled for managing cache
        private string cachedQueryTerm = "";
        private string cachedQueryLastCursorMark = "";
        private int cachedQueryItemCount = 0;
        private bool cachedQueryResultsComplete = false;
        private int minItemsToCache = 25; // minimum result prefetch

        // default client search values
        private string dResultType = "core", dSort = "", dEmail = "", dCusorMark = "*";
        private bool dUseSynonyms = true;


        public EBIPubmedService()
        {
            client = new EBIClient();
            cache = new Dictionary<string, EBIPubMedDetailedResult>();
        }

        void RefreshCache(string term)
        {
            cache.Clear(); cachedQueryItemCount = 0; cachedQueryLastCursorMark = ""; cachedQueryResultsComplete = false;

            cachedQueryTerm = term;
        }

        EBIPubMedDetailedResult Get(string pmid)
        {
            if (cache.ContainsKey(pmid)) return cache[pmid];

            string term = String.Format("ext_id:{0} SRC:MED", pmid);

            if (term == cachedQueryTerm) return null; // if we've tried this query before and the item isn't in the cache it obviously isn't available on the service!

            RefreshCache(term); // treat request for single publication as new query to be cached for future reference.

            responseWrapper results = client.searchPublications(term, dResultType, dCusorMark, "1", dSort, false, dEmail);

            if (results.hitCount == 0) return null;
            if (results.hitCount > 1) throw new ArgumentException("Pubmed ID is not unique; please recheck value and try again.");

            EBIPubMedDetailedResult p = new EBIPubMedDetailedResult(results.resultList[0]);
            cache.Add(p.Id, p);
            return p;
        }

        public IEnumerable<PubmedResult> Search(string term, int page, int pageSize, out int count)
        {
            // if last page of results is < pageSize we will need to copy fewer results from cache.
            int outputSize = pageSize;
            string resultsToReturn = Math.Max(pageSize, minItemsToCache).ToString();

            if (term != cachedQueryTerm)
            {

                RefreshCache(term);

                responseWrapper profile = client.profilePublications(term, "source", dUseSynonyms, dEmail);
                cachedQueryItemCount = profile.profileList.source.Where(p => p.name == "MED").First().count;

                if (cachedQueryItemCount > 0)
                {
                    responseWrapper results = client.searchPublications(term, dResultType, dCusorMark, resultsToReturn, dSort, dUseSynonyms, dEmail);

                    foreach (result r in results.resultList.Where(r => r.source == "MED"))
                    {
                        cache.Add(r.pmid, new EBIPubMedDetailedResult(r));
                    }

                    // results sets which are smaller than resultsToReturn still set a cursor mark, so need to use counts to determine correct cache settings:
                    if (results.resultList.Length < int.Parse(resultsToReturn)) cachedQueryResultsComplete = true;
                    else cachedQueryLastCursorMark = results.nextCursorMark;
                }
                else
                {
                    cachedQueryResultsComplete = true; // if the query returns no results it's obviously complete!
                }

            }
            else if (((page + 1) * pageSize) > cache.Count && !cachedQueryResultsComplete)
            {
                // we retrieve a minimum of 25 items to reduce the number of round trips to the EBI service in the case of low page sizes
                // if pagesize is bigger than the minimum value it will be used instead so we always retrieve either enough values to fulfil the request,
                // or as many values as are left to be returned from the EBI service.
                responseWrapper newItems = client.searchPublications(term, dResultType, cachedQueryLastCursorMark, resultsToReturn, dSort, dUseSynonyms, dEmail);

                // add new items to the cache. We don't need to update the Count as this is based off the profile counts not the individual query counts or cache size.
                foreach (result r in newItems.resultList.Where(r => r.source == "MED"))
                {
                    cache.Add(r.pmid, new EBIPubMedDetailedResult(r));
                }

                // update flags/cursor to indicate whether the results set is complete
                if (newItems.nextCursorMark == cachedQueryLastCursorMark) cachedQueryResultsComplete = true;
                else cachedQueryLastCursorMark = newItems.nextCursorMark;
            }

            // check whether we can serve full request from cache and update output size if not:
            if (cache.Count - (page * pageSize) < pageSize) outputSize = cache.Count - (page * pageSize);

            // serve request from cache:

            count = cachedQueryItemCount;
            PubmedResult[] output = new PubmedResult[outputSize];
            Array.Copy(cache.Values.Select(e => e.PubmedResult).ToArray(), page * pageSize, output, 0, outputSize);
            return output.ToList();
        }

        public PubmedResult Summary(string pmid)
        {
            EBIPubMedDetailedResult p = Get(pmid);
            if (p == null) return null;
            return p.PubmedResult;
        }

        public PubmedDetails Details(string pmid)
        {
            EBIPubMedDetailedResult p = Get(pmid);
            if (p == null) return null;
            return p.PubmedDetails;
        }

        public IEnumerable<string> MeshTerms(string pmid)
        {
            EBIPubMedDetailedResult p = Get(pmid);
            if (p == null) return null;
            return p.PubmedDetails.MeshTerms;
        }


        private class EBIPubMedDetailedResult
        {
            public string Id { get; }
            public string Title { get; }
            public IEnumerable<string> Authors { get; }
            public string Journal { get; }
            public string PublicationDate { get; }
            public IList<string> MeshTerms { get; }

            public EBIPubMedDetailedResult(result r)
            {
                Id = r.pmid;
                Title = r.title;
                Authors = r.authorList.Select(a => a.fullName).ToList();
                Journal = r.journalTitle;
                PublicationDate = r.firstPublicationDate;
                MeshTerms = r.meshHeadingList.Select(mh => mh.descriptorName).ToList();
            }

            public PubmedResult PubmedResult
            {
                get {
                    PubmedResult p = new PubmedResult();
                    p.Id = this.Id;
                    p.Title = this.Title;
                    p.Journal = this.Journal;
                    p.PublicationDate = this.PublicationDate;
                    p.Authors = this.Authors;

                    return p;
                }
            }

            public PubmedDetails PubmedDetails {
                get {
                    PubmedDetails p = new PubmedDetails();
                    p.Journal = this.Journal;
                    p.PublicationDate = this.PublicationDate;
                    p.Authors = this.Authors.ToList();
                    p.MeshTerms = this.MeshTerms;

                    return p;
                }
            }
        }
    }
}
