using KpsMernisServisWeb;
namespace WebServis.KpsMernis
{
    public class MernisKimlikDogrulama
    {
        public bool KpsKimlikDogrula(decimal TC, string Ad, string Soyad, DateTime DogumYil)


        {
            
            long Tckimlik = long.Parse(TC.ToString());
            int Dogum = int.Parse(DogumYil.Year.ToString());

            var client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            var respons = client.TCKimlikNoDogrulaAsync(Tckimlik, Ad, Soyad, Dogum);
            var result = respons.Result.Body.TCKimlikNoDogrulaResult;



            return result;

        }
    }
}
