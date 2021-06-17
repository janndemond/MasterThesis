using LINQtoCSV;
using Microsoft.VisualStudio.Services.WebApi;

namespace TestRestAPI
{
    public class WorkItem
    {
        private string _id;
        private string _status;
        private string _statusChange;
        private string _iteration;

       


        private string _workItemType;
        private string _priority;
        private string _area;
        private string _fileCount;
        private string _hyperLinkCount;
        private string _commentCount;
        private string _acceptedDate;
        private string _activatedDate;
        private string _changedDate;
        private string _createdDate;
        private string _closedDate;
        private string _finishDate;
        private string _resolvedDate;
        private string _reviewedDate;
        private string _acceptedBy;
        private string _activatedBy;
        private string _changedBy;
        private string _createdBy;
        private string _closedBy;
        private string _finishBy;
        private string _resolvedBy;
        private string _reviewedBy;

        [CsvColumn(Name = "Id", FieldIndex = 1)]
        public string Id
        {
            get => _id?? "";
            set => _id = value;
        }
        [CsvColumn(Name = "Status", FieldIndex =4 )]
        public string Status
        {
            get => _status?? "";
            set => _status = value;
        }
        [CsvColumn(Name = "StatusChange", FieldIndex = 5)]
        public string StatusChange
        {
            get => _statusChange?? "";
            set => _statusChange = value;
        }
        [CsvColumn(Name = "WorkItemType", FieldIndex = 2)]
        public string WorkItemType
        {
            get => _workItemType?? "";
            set => _workItemType = value;
        }

        [CsvColumn(Name = "Priority", FieldIndex = 3)]
        public string Priority
        {
            get => _priority?? "";
            set => _priority = value;
        }
        [CsvColumn(Name = "Area", FieldIndex = 6)]
        public string Area
        {
            get => _area?? "";
            set => _area = value;
        }
        [CsvColumn(Name = "Iteration", FieldIndex = 7)]
        public string Iteration
        {
            get => _iteration?? "";
            set => _iteration = value;
        }
        [CsvColumn(Name = "FileCount", FieldIndex = 8)]
        public string FileCount
        {
            get => _fileCount?? "";
            set => _fileCount = value;
        }
        [CsvColumn(Name = "HyperLinkCount", FieldIndex = 9)]
        public string HyperLinkCount
        {
            get => _hyperLinkCount?? "";
            set => _hyperLinkCount = value;
        }
        [CsvColumn(Name = "CommentCount", FieldIndex = 10)]
        public string CommentCount
        {
            get => _commentCount?? "";
            set => _commentCount = value;
        }
        [CsvColumn(Name = "AcceptedDate", FieldIndex = 11)]
        public string AcceptedDate
        {
            get => _acceptedDate?? "";
            set => _acceptedDate = value;
        }
        [CsvColumn(Name = "ActivatedDate", FieldIndex = 13)]
        public string ActivatedDate
        {
            get => _activatedDate?? "";
            set => _activatedDate = value;
        }
        [CsvColumn(Name = "ChangedDate", FieldIndex = 15)]
        public string ChangedDate
        {
            get => _changedDate?? "";
            set => _changedDate = value;
        }
        [CsvColumn(Name = "CreatedDate", FieldIndex = 17)]
        public string CreatedDate
        {
            get => _createdDate?? "";
            set => _createdDate = value;
        }
        [CsvColumn(Name = "ClosedDate", FieldIndex = 19)]
        public string ClosedDate
        {
            get => _closedDate?? "";
            set => _closedDate = value;
        }
        [CsvColumn(Name = "FinishDate", FieldIndex = 21)]
        public string FinishDate
        {
            get => _finishDate?? "";
            set => _finishDate = value;
        }
        [CsvColumn(Name = "ResolvedDate", FieldIndex = 23)]
        public string ResolvedDate
        {
            get => _resolvedDate?? "";
            set => _resolvedDate = value;
        }
        [CsvColumn(Name = "ReviewedDate", FieldIndex = 25)]
        public string ReviewedDate
        {
            get => _reviewedDate?? "";
            set => _reviewedDate = value;
        }
        [CsvColumn(Name = "AcceptedBy", FieldIndex = 12)]
        public string AcceptedBy
        {
            get => _acceptedBy?? "";
            set => _acceptedBy = value;
        }
        [CsvColumn(Name = "ActivatedBy", FieldIndex = 14)]
        public string ActivatedBy
        {
            get => _activatedBy?? "";
            set => _activatedBy = value;
        }
        [CsvColumn(Name = "ChangedBy", FieldIndex = 16)]
        public string ChangedBy
        {
            get => _changedBy?? "";
            set => _changedBy = value;
        }
        [CsvColumn(Name = "CreatedBy", FieldIndex = 18)]
        public string CreatedBy
        {
            get => _createdBy?? "";
            set => _createdBy = value;
        }
        [CsvColumn(Name = "ClosedBy", FieldIndex = 20)]
        public string ClosedBy
        {
            get => _closedBy?? "";
            set => _closedBy = value;
        }
        [CsvColumn(Name = "FinishBy", FieldIndex = 22)]
        public string FinishBy
        {
            get => _finishBy?? "";
            set => _finishBy = value;
        }
        [CsvColumn(Name = "ResolvedBy", FieldIndex = 24)]
        public string ResolvedBy
        {
            get => _resolvedBy?? "";
            set => _resolvedBy = value;
        }
        [CsvColumn(Name = "ReviewedBy", FieldIndex = 26)]
        public string ReviewedBy
        {
            get => _reviewedBy?? "";
            set => _reviewedBy = value;
        }
    }
}