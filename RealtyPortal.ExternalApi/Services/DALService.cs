using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RealtyPortal.ExternalApi.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyPortal.ExternalApi.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<object> _playlistCollection;
        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playlistCollection = database.GetCollection<object>(mongoDBSettings.Value.CollectionName);
        }
        public async Task<List<string>> GetAsync() { return null; }
        public async Task CreateAsync(object playlist) { }
        public async Task AddToPlaylistAsync(string id, string movieId) { }
        public async Task DeleteAsync(string id) { }
    }
}
