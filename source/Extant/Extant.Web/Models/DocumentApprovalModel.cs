using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdditionalDocumentType = Extant.Data.Entities.DocumentType;

namespace Extant.Web.Models
{
    public class DocumentApprovalModel
    {
        public int StudyId { get; set; }
        public string StudyTitle { get; set; }
        public DocumentType Document { get; set; }
        public AdditionalDocumentDetails DocumentDetails { get; set; }

        public class AdditionalDocumentDetails
        {
            public AdditionalDocumentType Type { get; set; }
            public string Description { get; set; }
            public string FileName { get; set;  }
        }

        public enum DocumentType
        {
            PatientInformationLeaflet,
            ConsentForm,
            DataAccessPolicy,
            AdditionalDocument
        }
    }
}