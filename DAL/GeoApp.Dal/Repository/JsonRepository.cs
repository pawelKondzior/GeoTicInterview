using GeoApp.DAL.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpRepository.Repository;
using SharpRepository.Repository.FetchStrategies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeoApp.DAL.Repository
{
    /// <summary>
    /// Problemem jest, że ładuje wszystkie pliki na raz do pamięci
    /// Jednak nie znam dokładnie specyfiki problemu, 
    /// więc załaduje wszystkie przy próbie odczytu skoro za klucz zakładamy unitId i np IRepository mogło by byc użyte a pliki w MS SQL
    /// </summary>
    public class JsonRepository : LinqRepositoryBase<GeoInformation, string>
    { 
        List<GeoInformation> AllItems {get; set;}
        string[] FileNames { get; set; }
        public JsonRepository(string[] fileNames)
        {
            FileNames = fileNames;

            AllItems = new List<GeoInformation>();

        }

        private void LoadItems(string[] fileNames)
        {
            foreach(var file in fileNames)
            {
                var toAdd = LoadJsonFile(file);

                if (toAdd != null)
                {
                    AllItems.AddRange(toAdd);
                }
                
            }
        }

        private List<GeoInformation> LoadJsonFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            List<GeoInformation> result;

            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                result = (List<GeoInformation>)serializer.Deserialize(file, typeof(List<GeoInformation>));
            }

            return result;
        }

        public override void Dispose()
        {
            
        }

        protected override void AddItem(GeoInformation entity)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<GeoInformation> BaseQuery(IFetchStrategy<GeoInformation> fetchStrategy = null)
        {

            LoadItems(FileNames);

            return AllItems.AsQueryable< GeoInformation>();
            
        }

        protected override void DeleteItem(GeoInformation entity)
        {
            throw new NotImplementedException();
        }

        protected override void SaveChanges()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateItem(GeoInformation entity)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<string> GetMails()
        //{
        //    return this.AsQueryable().Where(c => c.Emails != null & c.Emails.Any()).SelectMany(c => c.Emails).Select(m => m.EmailAddress)
        //        .Distinct();
        //}
    }
}
