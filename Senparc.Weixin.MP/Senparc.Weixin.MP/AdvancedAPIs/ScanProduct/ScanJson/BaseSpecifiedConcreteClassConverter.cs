using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class BaseSpecifiedConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Product_Brand_Base_Info).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
            return base.ResolveContractConverter(objectType);
        }
    }


    public class ScanProductActionListConverter : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new BaseSpecifiedConcreteClassConverter() };

        private Type type1 = typeof(Product_Brand_Action_Info_Base);
        private Type type2 = typeof(Product_Brand_Module_Info_Base);
        public override bool CanConvert(Type objectType)
        {
            return (objectType == type1 || objectType == type2);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //JContainer lJContainer = default(JContainer);
            //if (reader.TokenType == JsonToken.StartObject)
            //{
            //    lJContainer = JObject.Load(reader);
            //    existingValue = Convert.ChangeType(existingValue, objectType);
            //    existingValue = Activator.CreateInstance(objectType);

            //    serializer.Populate(lJContainer.CreateReader(), existingValue);
            //}

            //return existingValue;

            //var jo = JObject.ReadFrom(reader);
            ////existingValue = Convert.ChangeType(existingValue, objectType);
            ////existingValue = Activator.CreateInstance(objectType);
            ////serializer.Populate(jo.CreateReader(), existingValue);

            //switch (jo["type"].Value<string>())
            //{
            //    case "media":
            //        return JsonConvert.DeserializeObject<Product_Brand_Action_Info_Media>(jo.ToString(), SpecifiedSubclassConversion);
            //    case "text":
            //        return JsonConvert.DeserializeObject<Product_Brand_Action_Info_Text>(jo.ToString(), SpecifiedSubclassConversion);
            //    case "link":
            //        return JsonConvert.DeserializeObject<Product_Brand_Action_Info_Link>(jo.ToString(), SpecifiedSubclassConversion);
            //    case "user":
            //        return JsonConvert.DeserializeObject<Product_Brand_Action_Info_User>(jo.ToString(), SpecifiedSubclassConversion);
            //    case "card":
            //        return JsonConvert.DeserializeObject<Product_Brand_Action_Info_Card>(jo.ToString(), SpecifiedSubclassConversion);
            //    case "price":
            //        return jo.ToObject<Product_Brand_Action_Info_Price>();
            //    case "product":
            //        return JsonConvert.DeserializeObject<Product_Brand_Action_Info_Product>(jo.ToString(), SpecifiedSubclassConversion);
            //    case "store":
            //        return JsonConvert.DeserializeObject<Product_Brand_Action_Info_Store>(jo.ToString(), SpecifiedSubclassConversion);
            //    case "recommend":
            //        return JsonConvert.DeserializeObject<Product_Brand_Action_Info_Recommend>(jo.ToString(), SpecifiedSubclassConversion);
            //    case "anti_fake":
            //        return jo.ToObject<Product_Brand_Module_Info_Item_AntiFake>();

            //    default:
            //        throw new NotImplementedException("没有相应的转换器");
            //}


            var jo = JObject.ReadFrom(reader);

            var actionOption = jo["type"].Value<string>().ToString();

            if (actionOption == "link")
            {
                if (((Newtonsoft.Json.Linq.JContainer)(jo)).Count == 5)
                    actionOption = "linkimage";
            }

            var n = string.Empty;
            if (objectType == type1)
                n = "Senparc.Weixin.MP.AdvancedAPIs.ScanProduct.Product_Brand_Action_Info_" + ToTitleCase(actionOption);
            else if (objectType == type2)
                n = "Senparc.Weixin.MP.AdvancedAPIs.ScanProduct.Product_Brand_Module_Info_" + ToTitleCase(actionOption);

            var myType = Type.GetType(n);


            MethodInfo method = jo.GetType().GetMethods().First(m => m.Name == "ToObject" && m.IsGenericMethod);
            MethodInfo generic = method.MakeGenericMethod(myType);
            var o = generic.Invoke(jo, null);
            return o;
        }

        public static string ToTitleCase(string str)
        {
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            var text = cultureInfo.TextInfo;
            string result = text.ToTitleCase(str);

            return result;
        }


        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }
    }
}
