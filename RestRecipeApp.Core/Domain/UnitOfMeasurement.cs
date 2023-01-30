using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RestRecipeApp.Core.Domain;

[JsonConverter(typeof(StringEnumConverter))]
public enum UnitOfMeasurement
{
    ml,
    cl,
    dl,
    l,
    g,
    kg,
    unit,
    mm,
    cm,
    m,
    tl,
    el,
    pinch,
    cup,
    pint,
}