namespace UI.Mappers;

public interface IConverter<in TSource, out TDestination>
{
  TDestination Convert(TSource sourceObject);
}