
using BlazorCMSRouter.Rendering;

namespace BlazorCMSRouter.Routing;

public class RouteManager
{
  public Route[] Routes { get; private set; }

  public void Initialize()
  {
    // TODO - load all the pages from the database

    // NOTE - the routes already decorated with an @page directive "work" -- and render our custom content
    //      - the routes that aren't covered with an existing @page directive return 404
    var pages = new string[] { "", "blah", "error", "weather", "counter" };

    Routes = pages.Select(p => new Route
    {
      Uri = p,
      Handler = typeof(PageContentRenderer)
    }).ToArray();
  }

  public MatchResult Match(string uri)
  {
    // TODO - simplify me - we probably don't need segments for our use case, etc...

    // if (segments.Length == 0)
    // {
    //   // var indexRoute = Routes.SingleOrDefault(x => x.Handler.FullName.ToLower().EndsWith("index"));
    //   return MatchResult.Match(new Route() { Handler = typeof(Pages.Home), Uri = segments });
    // }

    foreach (var route in Routes)
    {
      var matchResult = route.Match(uri);

      if (matchResult.IsMatch)
      {
        return matchResult;
      }
    }

    return MatchResult.NoMatch();
  }
}
