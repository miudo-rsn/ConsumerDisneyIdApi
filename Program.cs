using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumerDisneyIdApi
{
    // classe que representa o personagem (parte "data" do json)
    public class Personagem
    {
        public int _id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
    }

    // classe que representa o json inteiro que a API devolve
    public class Resposta
    {
        public Personagem data { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            // endereco da API com o _id = 423
            string url = "https://api.disneyapi.dev/character/423";

            try
            {
                HttpClient client = new HttpClient();

                // faz a chamada na API
                HttpResponseMessage response = await client.GetAsync(url);

                // verifica se deu certo
                if (response.IsSuccessStatusCode)
                {
                    // le o json como texto
                    string json = await response.Content.ReadAsStringAsync();

                    // transforma o json em objeto
                    Resposta resposta = JsonSerializer.Deserialize<Resposta>(json);

                    // imprime os dados na tela
                    Console.WriteLine("Nome:");
                    Console.WriteLine(resposta.data.name);
                    Console.WriteLine("Imagem:");
                    Console.WriteLine(resposta.data.imageUrl);
                }
                else
                {
                    Console.WriteLine("Erro ao chamar a API: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deu erro: " + ex.Message);
            }

            Console.ReadKey();
        }
    }
}
