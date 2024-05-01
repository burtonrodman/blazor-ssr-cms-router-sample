namespace BlazorCMSRouter.Routing;

public class Route
{
  public string Uri { get; set; }
  public Type Handler { get; set; }

  public MatchResult Match(string uri)
  {
    // if (uri.Length != Uri.Length)
    // {
    //   return MatchResult.NoMatch();
    // }

    // for (var i = 0; i < Uri.Length; i++)
    // {
    //   if (string.Compare(segments[i], Uri[i], StringComparison.OrdinalIgnoreCase) != 0)
    //   {
    //     return MatchResult.NoMatch();
    //   }
    // }

    if (string.Compare(uri, this.Uri, StringComparison.InvariantCultureIgnoreCase) == 0)
    {
      return MatchResult.Match(this);
    }
    else
    {
      return MatchResult.NoMatch();
    }
  }
}