using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TestReadProperties: MonoBehaviour {
    public ClassA[] a;

    [ContextMenu("Count property")]
    public void CountProperties() {
        Type type = typeof(ClassA);
        int attributeCount = 0;

        foreach (FieldInfo fieldInfo in type.GetFields()) {
            //Debug.Log("FieldInfo : " + fieldInfo.Name + " " + fieldInfo.FieldType.IsClass + " | : " + fieldInfo.FieldType + "|" + fieldInfo.FieldType.IsGenericType + "|" + fieldInfo.FieldType.IsGenericTypeDefinition);

            if (fieldInfo.FieldType.IsGenericType) {
                //if(fieldInfo.FieldType.)
                Debug.Log("Type is list: " + fieldInfo.FieldType.ToString());
                Type a = fieldInfo.FieldType.GetGenericArguments()[0];

                Debug.Log("Type: " + a + " | is IsAbstract? " + a + " | is IsClass? " + a.IsClass);

            }
        }

        //System.Reflection.MemberInfo info = typeof(ClassA);
        //object[] attributes = info.GetCustomAttributes(true);
        //var attributeCount = attributes.Length;

        Debug.Log("Number FieldInfo: " + attributeCount + " | " + type.GetFields().Length);

    }
}

[Serializable]
public class ClassA {
    //public TypeTest A;
    //public int B;
    //public B S;
    //public string F;
    public List<string> SAD;
    public List<B> SADs;
    //public string[] s;
}

public enum TypeTest {
    A,
    B,
    C
}

public class B {
    public bool a;
    public int aa;
}
