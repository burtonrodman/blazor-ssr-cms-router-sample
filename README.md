# UPDATE:  Use fallback route
See this [GitHub issue](https://github.com/dotnet/aspnetcore/issues/53135)


# Blazor Custom Router Sample

in RouteManager.cs

    // NOTE - the routes already decorated with an @page directive "work" -- and render our custom content
    //      - the routes that aren't covered with an existing @page directive return 404
    var pages = new string[] { "", "blah", "error", "weather", "counter" };