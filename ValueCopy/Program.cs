using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueCopy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CopyDemo();
            
            Console.ReadKey();
        }


        private static void CopyDemo()
        {
            CopyDemoClass DemoA=new CopyDemoClass();

            Console.WriteLine("DemoA中所有字段的原始值：");

            Console.WriteLine("int          {0}",DemoA.intValue);
            Console.WriteLine("string       {0}", DemoA.strValue);
            Console.WriteLine("Enum         {0}", DemoA.pEnum);
            Console.WriteLine("struct       {0}", DemoA.strValue);
            Console.WriteLine("Class        {0}", DemoA.pClass.name);
            Console.WriteLine("int[]        {0}", DemoA.pIntArray[0]);
            Console.WriteLine("string[]     {0}", DemoA.pStringArray[0]);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("输入'1'进行浅拷贝，输入'2'进行浅拷贝,请输入：");
            if (Console.ReadLine() == "1")
            {
                ShallowCopyDemo(DemoA);
            }
            else
            {
                DeepCopyDemo(DemoA);
            }

        }

        private static void ShallowCopyDemo(CopyDemoClass DemoA)
        {
            Console.WriteLine("浅拷贝：====================================");
            CopyDemoClass DemoB = DemoA.Clone() as CopyDemoClass;

            DemoB.intValue = 2;
            Console.WriteLine(string.Format("int      [A:{0}]  [B:{1}]", DemoA.intValue, DemoB.intValue));
            DemoB.strValue = "2";
            Console.WriteLine(string.Format("string   [A:{0}]  [B:{1}]", DemoA.strValue, DemoB.strValue));
            DemoB.pEnum = CopyDemoClass.PersonEnum.EnumB;
            Console.WriteLine(string.Format("Enum     [A:{0}]  [B:{1}]", DemoA.pEnum, DemoB.pEnum));
            DemoB.personStruct.structValue = 2;
            Console.WriteLine(string.Format("struct   [A:{0}]  [B:{1}]", DemoA.personStruct.structValue, DemoB.personStruct.structValue));
            DemoB.pClass.name = "2";
            Console.WriteLine(string.Format("Class    [A:{0}]  [B:{1}]", DemoA.pClass.name, DemoB.pClass.name));
            DemoB.pIntArray[0] = 2;
            Console.WriteLine(string.Format("int[]    [A:{0}]  [B:{1}]", DemoA.pIntArray[0], DemoB.pIntArray[0]));
            DemoB.pStringArray[0] = "2";
            Console.WriteLine(string.Format("string[] [A:{0}]  [B:{1}]", DemoA.pStringArray[0], DemoB.pStringArray[0]));
        }

        private static void DeepCopyDemo(CopyDemoClass DemoA)
        {
            Console.WriteLine("深拷贝：====================================");
            CopyDemoClass DemoC = DeepCopyClass.DeepCopyWithDC(DemoA);

            DemoC.intValue = 2;
            Console.WriteLine(string.Format("int      [A:{0}]  [C:{1}]", DemoA.intValue, DemoC.intValue));
            DemoC.strValue = "2";
            Console.WriteLine(string.Format("string   [A:{0}]  [C:{1}]", DemoA.strValue, DemoC.strValue));
            DemoC.pEnum = CopyDemoClass.PersonEnum.EnumB;
            Console.WriteLine(string.Format("Enum     [A:{0}]  [C:{1}]", DemoA.pEnum, DemoC.pEnum));
            DemoC.personStruct.structValue = 2;
            Console.WriteLine(string.Format("struct   [A:{0}]  [C:{1}]", DemoA.personStruct.structValue, DemoC.personStruct.structValue));
            DemoC.pClass.name = "2";
            Console.WriteLine(string.Format("Class    [A:{0}]  [C:{1}]", DemoA.pClass.name, DemoC.pClass.name));
            DemoC.pIntArray[0] = 2;
            Console.WriteLine(string.Format("int[]    [A:{0}]  [C:{1}]", DemoA.pIntArray[0], DemoC.pIntArray[0]));
            DemoC.pStringArray[0] = "2";
            Console.WriteLine(string.Format("string[] [A:{0}]  [C:{1}]", DemoA.pStringArray[0], DemoC.pStringArray[0]));
        }
        
    }
}
