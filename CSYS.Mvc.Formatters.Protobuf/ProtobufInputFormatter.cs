using Google.Protobuf;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Threading.Tasks;

namespace CSYS.Mvc.Formatters.Protobuf
{
    public class ProtobufInputFormatter : InputFormatter
    {
        static MediaTypeHeaderValue protoMediaType = MediaTypeHeaderValue.Parse("application/x-protobuf");

        public ProtobufInputFormatter()
        {
            SupportedMediaTypes.Add(protoMediaType);
        }

        //public override bool CanRead(InputFormatterContext context)
        //{
        //    return context.HttpContext.Request.ContentType == "application/x-protobuf";
        //}

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            try
            {
                var request = context.HttpContext.Request;
                var obj = (IMessage)Activator.CreateInstance(context.ModelType);
                obj.MergeFrom(request.Body);

                return InputFormatterResult.SuccessAsync(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
                return InputFormatterResult.FailureAsync();
            }
        }
    }
}
