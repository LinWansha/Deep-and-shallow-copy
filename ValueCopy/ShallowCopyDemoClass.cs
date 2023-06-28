using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueCopy
{
    [Serializable]
    /// <summary>
    /// 浅拷贝类
    /// </summary>
    internal class CopyDemoClass : ICloneable
    {
        public int intValue = 1;

        public string strValue = "1";

        public PersonEnum pEnum = PersonEnum.EnumA;

        public PersonStruct personStruct= new PersonStruct() { structValue=1};

        public Person pClass = new Person("1");

        public int[] pIntArray= new int[] {1};

        public string[] pStringArray = new string[] { "1" };


        public object Clone()
        {
            //MemberwiseClone 方法返回当前的浅拷贝副本
            return this.MemberwiseClone();
        }
        [Serializable]
        public class Person
        {
            public string name;
            public Person()
            {

            }
            public Person(string name)
            {
                this.name = name;
            }
        }
        [Serializable]
        public enum PersonEnum
        {
            EnumA = 0,
            EnumB = 1
        }
        [Serializable]
        public struct PersonStruct
        {
            public int structValue;
        }
    }
}
