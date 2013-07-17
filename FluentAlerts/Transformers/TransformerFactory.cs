//using FluentAlerts.Transformers.Formatters;
//using FluentAlerts.Transformers.Strategies;
//using FluentAlerts.Transformers.TypeInformers;

//namespace FluentAlerts.Transformers
//{
//    public interface ITransformerFactory
//    {
//        ITransformer<string> Create();
//        ITransformer<string> CreateNameTypeValue();
//    }

//    public class TransformerFactory: ITransformerFactory
//    {
//        private readonly IAlertBuilderFactory _alertBuilderFactory;
//        public TransformerFactory(IAlertBuilderFactory alertBuilderFactory)
//        {
//            _alertBuilderFactory = alertBuilderFactory;
//        }

//        public ITransformer<string> Create()
//        {
//            return new NameValueRowTransformer(new DefaultTransformStrategy(),
//                                               new DefaultTypeInfoSelector(),
//                                               new DefaultToStringFormatter(),  
//                                               _alertBuilderFactory);
//        }

//        public ITransformer<string> CreateNameTypeValue()
//        {
//            return new NameTypeValueRowTransformer(new DefaultTransformStrategy(),
//                                               new DefaultTypeInfoSelector(),
//                                               new DefaultToStringFormatter(),
//                                               _alertBuilderFactory);
//        }
//    }
//}
