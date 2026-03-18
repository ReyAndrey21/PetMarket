using PetMarket.Models;
using PetMarket.Services.Interfaces;

namespace PetMarket.Services
{
    public class SearchService : ISearchService
    {
        public List<Product> Search(List<Product> allProducts, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return allProducts;

            var searchTerms = query.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var results = new List<(Product Product, double Score)>();

            var documents = allProducts.Select(p =>
                p.NameProduct.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList()
            ).ToList();

            foreach (var product in allProducts)
            {
                double totalScore = CalculateProductScore(product, searchTerms, documents);

                if (totalScore > 0)
                    results.Add((product, totalScore));
            }

            return results.OrderByDescending(r => r.Score)
                          .Select(r => r.Product)
                          .ToList();
        }

        private double CalculateProductScore(Product product, string[] searchTerms, List<List<string>> documents)
        {
            double score = 0;
            var content = product.NameProduct.ToLower();
            var termsInDoc = content.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            int totalDocs = documents.Count;

            foreach (var term in searchTerms)
            {
                
                double tf = (double)termsInDoc.Count(t => t.Contains(term)) / (termsInDoc.Count > 0 ? termsInDoc.Count : 1);

                
                int docsWithTerm = documents.Count(d => d.Any(t => t.Contains(term)));
                double idf = Math.Log((double)totalDocs / (docsWithTerm == 0 ? 1 : docsWithTerm));

                double weight = DetermineTermWeight(content, term);

                score += tf * idf * weight;
            }

            return score;
        }

        private double DetermineTermWeight(string content, string term)
        {
            
            if (content.Equals(term))
            {
                return 3.0; 
            }

            if (content.StartsWith(term))
            {
                return 2.0; 
            }

            return 1.0; 
        }
    }
}