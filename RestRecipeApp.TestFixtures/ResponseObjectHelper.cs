using LanguageExt;
using Newtonsoft.Json;

namespace RestRecipeAPI.TestFixtures;

public class ResponseObjectHelper
{
    public static async Task<Option<T>> GetResponseObject<T>(HttpResponseMessage response)
    {
        if (response == null)
        {
            throw new NullReferenceException();
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            return JsonConvert.DeserializeObject<T>(responseContent);
        }
        catch (Exception e)
        {
            return Option<T>.None;
        }
    }

    
}