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

        // maintenance variables for managing cache
        private string cachedQueryTerm = "";
        private string cachedQueryLastCursorMark = "";
        private int cachedQueryItemCount = 0;
        private int minItemsToCache = 25; // minimum result prefetch

        // default client search values
        private string dResultType = "core", dSort = "", dEmail = "", dCusorMark = "*";
        private string dUseSynonyms = true.ToString();


        public EBIPubmedService()
        {
            client = new EBIClient();
            cache = new Dictionary<string, EBIPubMedDetailedResult>();
        }

        void RefreshCache(string term)
        {
            cache.Clear(); cachedQueryItemCount = 0; cachedQueryLastCursorMark = dCusorMark;

            cachedQueryTerm = term;
        }

        EBIPubMedDetailedResult Get(string pmid)
        {
            if (cache.ContainsKey(pmid)) return cache[pmid];

            string term = String.Format("ext_id:{0} SRC:MED", pmid);

            if (term == cachedQueryTerm) return null; // if we've tried this query before and the item isn't in the cache it obviously isn't available on the service!

            RefreshCache(term); // treat request for single publication as new query to be cached for future reference.

            responseWrapper results = client.searchPublications(term, dResultType, dCusorMark, "1", dSort, "false", dEmail);

            if (results.hitCount == 0) return null;
            if (results.hitCount > 1) throw new ArgumentException("Pubmed ID is not unique; please recheck value and try again.");

            EBIPubMedDetailedResult p = new EBIPubMedDetailedResult(results.resultList[0]);
            cache.Add(p.Id, p);
            return p;
        }

        public IEnumerable<PubmedResult> Search(string term, int page, int pageSize, out int count)
        {

            // check if we've cached this query before; if not, clean the cache and profile the new query's resultset.
            if (term != cachedQueryTerm) {

                RefreshCache(term);

                responseWrapper profile = client.profilePublications(term, "source", dUseSynonyms, dEmail);
                cachedQueryItemCount = profile.profileList.source.Where(p => p.name == "MED").First().count;
            }

            count = cachedQueryItemCount;

            if (count == 0) return new List<PubmedResult>(); // there's no results to fetch!

            int lastRequiredRecord = Math.Min(cachedQueryItemCount, (page + 1) * pageSize); 

            // as results are not always PubMed, we should aim to retrieve the most results possible from the query

            // fill the cache in a loop as Pubmed are a subset of returned results and a single pass may not retrieve enough results:
            while (cache.Count < lastRequiredRecord) {

                int resultsToReturn = Math.Max(lastRequiredRecord - cache.Count, minItemsToCache);

                responseWrapper results = client.searchPublications(term, dResultType, cachedQueryLastCursorMark, resultsToReturn.ToString(), dSort, dUseSynonyms, dEmail);

                foreach (result r in results.resultList.Where(r => r.source == "MED")) {
                    cache.Add(r.pmid, new EBIPubMedDetailedResult(r));
                }

                // Sanity check - if we retrieve less results than requested but the cache isn't sufficiently full something has gone wrong!
                if (results.resultList.Length < resultsToReturn && cache.Count < lastRequiredRecord) throw new Exception(String.Format("All results returned but cache not filled - profiling error in EBIPubMedService for query \"{0}\"", term));

                cachedQueryLastCursorMark = results.nextCursorMark;
            }

            // check whether we can serve full request from cache and set output size accordingly:
            int outputSize = Math.Min(cache.Count - (page * pageSize), pageSize);

            // serve request from cache:

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
                Authors = r.authorList == null? new List<string> { r.authorString } : r.authorList.Select(a => a.fullName).ToList();
                Journal = r.journalTitle;
                PublicationDate = r.firstPublicationDate;
                if (r.meshHeadingList == null || r.meshHeadingList.Length == 0) MeshTerms = new List<string> { };
                else MeshTerms = r.meshHeadingList.Select(mh => mh.descriptorName).ToList();
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
