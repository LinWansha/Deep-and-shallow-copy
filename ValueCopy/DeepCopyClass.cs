using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ValueCopy
{
    /// <summary>
    /// 深拷贝类
    /// </summary>
    public class DeepCopyClass
    {
        /// <summary>
        /// 反射实现深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopyWithReflection<T>(T obj)
        {
            Type type=obj.GetType();

            if(obj is string||type.IsValueType) return obj;

            if (type.IsArray)
            {
                Type elementType = Type.GetType(type.FullName.Replace("[]", string.Empty));

                var array = obj as Array;

                Array copied = Array.CreateInstance(elementType, array.Length);

                for (int i = 0; i < array.Length; i++)
                {
                    copied.SetValue(DeepCopyWithReflection(array.GetValue(i)), i);
                }
                return (T)Convert.ChangeType(copied,obj.GetType()); 
            }
            object retval=Activator.CreateInstance(obj.GetType());

            PropertyInfo[] properties=obj.GetType().GetProperties(BindingFlags.Public|BindingFlags.NonPublic|
                BindingFlags.Instance|BindingFlags.Static);

            foreach (PropertyInfo property in properties)
            {
                var propertyValue=property.GetValue(obj,null);
                
                if (propertyValue == null)
                    continue;
                property.SetValue(retval,DeepCopyWithReflection(propertyValue),null);
            }
            return (T) retval;
        }
        /// <summary>
        /// 利用Xml序列化和反序列化实现
        /// 注意：
        /// 1.被序列化的类必须公开
        /// 2.要序列化的类中必须有无参构造？
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopyWithXmlSerialzer<T>(T obj)
        {
            object retval;
            using(MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xml=new XmlSerializer(typeof(T));
                xml.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval=xml.Deserialize(ms);
                ms.Close();

            }
            return (T) retval;
        }
        /// <summary>
        /// 利用二进制序列化和反序列化实现
        /// 注意：
        /// 1.要序列化的目标类必须加 [Serializable] 可序列化标签
        /// （可以序列化非公开类）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopyWithBinarySerialize<T>(T obj)
        {
            object retval;
            using(MemoryStream ms= new MemoryStream())
            {
                BinaryFormatter bf=new BinaryFormatter();

                bf.Serialize(ms, obj);

                ms.Seek(0, SeekOrigin.Begin);

                retval=bf.Deserialize(ms);

                ms.Close();
            }

            return (T) retval;
        }
        /// <summary>
        /// DataContractSerializer
        /// 使用数据协定将类型实例序列化和反序列化为XML文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopyWithDC<T>(T obj)
        {
            object retval;

            using(MemoryStream ms=new MemoryStream())
            {
                DataContractSerializer ser=new DataContractSerializer(typeof(T));

                ser.WriteObject(ms, obj);

                ms.Seek(0, SeekOrigin.Begin);

                retval = ser.ReadObject(ms);

                ms.Close();
            }
            return (T) retval;  
        }
    }
}
