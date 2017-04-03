using Google.Protobuf;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSYS.Mvc.Formatters.Protobuf
{
    public class ProtobufOutputFormatter : OutputFormatter
    {
        static MediaTypeHeaderValue protoMediaType = MediaTypeHeaderValue.Parse("application/x-protobuf");

        public ProtobufOutputFormatter()
        {
            SupportedMediaTypes.Add(protoMediaType);
        }

        //public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        //{
        //    return true;
        //    //if (context.Object == null || !context.ContentType.IsSubsetOf(protoMediaType))
        //    //{
        //    //    return false;
        //    //}

        //    //// Check whether the given object is a proto-generated object
        //    //return context.ObjectType.GetTypeInfo()
        //    //    .ImplementedInterfaces
        //    //    .Where(i => i.GetTypeInfo().IsGenericType)
        //    //    .Any(i => i.GetGenericTypeDefinition() == typeof(IMessage<>));
        //}
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;

            var protoObj = context.Object as IMessage;
            var serialized = protoObj.ToByteArray();

            return response.Body.WriteAsync(serialized, 0, serialized.Length);
        }
    }
}
