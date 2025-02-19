using System.Reflection;
namespace Lab2.Services;
public class ObjectMapperService
{
    public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        TDestination destination = new TDestination();
        
        var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var destProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var destProp in destProperties)
        {
            string sourcePropName = destProp.Name;
            var mapFromAttr = destProp.GetCustomAttribute<MapFromAttribute>();
            if (mapFromAttr != null)
            {
                sourcePropName = mapFromAttr.SourcePropertyName;
            }
            var sourceProp = sourceProperties.FirstOrDefault(sp =>
                sp.Name.Equals(sourcePropName, StringComparison.OrdinalIgnoreCase));
            
            if (sourceProp != null && destProp.CanWrite)
            {
                object sourceValue = sourceProp.GetValue(source);
                destProp.SetValue(destination, sourceValue);
            }
        }
        return destination;
    }
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class MapFromAttribute(string sourcePropertyName) : Attribute
{
    public string SourcePropertyName { get; } = sourcePropertyName;
}
