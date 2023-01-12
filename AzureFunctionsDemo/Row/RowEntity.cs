using Azure;
using Azure.Data.Tables;
using AzureFunctionsDemo.File;
using System;

namespace AzureFunctionsDemo.Row
{
    internal class RowEntity : FileRowMessage, ITableEntity
    {
        public RowEntity(FileRowMessage fileRowMessage)
        {
            Id = fileRowMessage.Id;
            Email = fileRowMessage.Email;
            FirstName = fileRowMessage.FirstName;
            LastName = fileRowMessage.LastName;
            Profession = fileRowMessage.Profession;
        }

        public string PartitionKey { get => this.Profession; set => this.Profession = value; }
        public string RowKey
        {
            get => this.Id.ToString();
            set =>
                this.Id = Int32.Parse(value);
        }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
