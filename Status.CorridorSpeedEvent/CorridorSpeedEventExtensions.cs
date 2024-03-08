using Econolite.Ode.Status.Common.Compare;

namespace Econolite.Ode.Status.CorridorSpeedEvent;

public static class CorridorSpeedEventExtensions
{
    public const string CORRIDOR_SPEED_EVENT = "corridorspeedevent";
    public static Func<StatementProperty, IFuncCompare> IntStatusValueFuncCompare = (property) => new StatusValue<int>(property);
    private static Dictionary<string, Func<StatementProperty, IFuncCompare>> _comparison = new Dictionary<string, Func<StatementProperty, IFuncCompare>>();

    static CorridorSpeedEventExtensions()
    {

        _comparison.Add(CORRIDOR_SPEED_EVENT, IntStatusValueFuncCompare);
    }

    public static Func<bool> ToFuncCompare(this CorridorSegmentSpeedEvent status, StatementProperty property)
    {

        if (property.Name.ToLower() != CORRIDOR_SPEED_EVENT.ToLower())
        {
            return () => false;
        }

        var statementCompare = GetFuncCompare(property.Value);
        var funcCompare = statementCompare(property);
        return funcCompare.CompareTo(status.SegmentSpeed);

    }

    private static Func<StatementProperty, IFuncCompare> GetFuncCompare(string name)
    {
        if (_comparison.TryGetValue(name, out var result))
        {
            return result;
        }

        return (property) => new FalseFuncCompare();
    }
}