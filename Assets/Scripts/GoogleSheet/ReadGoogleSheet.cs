using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;

namespace AnhVuiVe {
    public class ReadGoogleSheet {
        public static void FillData<T>(string id, string gridId, Action<List<T>> calBack) where T : new() {
            GetTable(id, gridId, arr => {
                var type = typeof(T);
                List<T> lst = new List<T>();
                for (int i = 2; i < arr.Count; i++) {
                    var t = new T();
                    lst.Add(t);
                }

                var header = arr[1];

                for (int i = 0; i < header.Count; i++) {
                    if (header[i] != string.Empty) {
                        var property = type.GetField(header[i].Replace("\r", string.Empty));
                        if (property != null) {
                            for (int j = 0; j < lst.Count; j++) {
                                if (arr[j + 2] != null) {
                                    var value = arr[j + 2][i].Replace("\r", string.Empty);
                                    var x = property.FieldType;
                                    if (x.IsEnum) {
                                        object objValue = Enum.Parse(property.FieldType, value);
                                        property.SetValue(lst[j], objValue);
                                    } else if (String.IsNullOrEmpty(value)) {

                                    } else {
                                        object objValue = Convert.ChangeType(value, property.FieldType);
                                        property.SetValue(lst[j], objValue);
                                    }
                                }
                            }
                        }
                    }
                }

                calBack(lst);
            });
        }

        public static List<T> FillDataIsList<T>(List<List<string>> table) where T : new() {
            var type = typeof(T);
            List<T> lst = new List<T>();
            for (int i = 2; i < table.Count; i++) {
                var t = new T();
                lst.Add(t);
            }

            var header = table[1];

            for (int i = 0; i < header.Count; i++) {
                if (header[i] != string.Empty) {
                    var property = type.GetField(header[i].Replace("\r", string.Empty));
                    if (property != null) {
                        for (int j = 0; j < lst.Count; j++) {
                            if (table[j + 2] != null) {
                                var value = table[j + 2][i].Replace("\r", string.Empty);
                                var x = property.FieldType;
                                if (x.IsEnum) {
                                    object objValue = Enum.Parse(property.FieldType, value);
                                    property.SetValue(lst[j], objValue);
                                } else if (String.IsNullOrEmpty(value)) {

                                } else {
                                    object objValue = Convert.ChangeType(value, property.FieldType);
                                    property.SetValue(lst[j], objValue);
                                }
                            }
                        }
                    }
                }
            }

            return lst;
        }

        public static T FillDataHasList<T, T1>(string id, string gridId, List<List<string>> listDataLoaded = null) where T : new() where T1 : new() {
            List<List<string>> arr = new List<List<string>>();

            if (listDataLoaded != null)
                arr = listDataLoaded;
            else
                GetTable(id, gridId, list => arr = list);

            var type = typeof(T);
            var type1 = typeof(T1);
            T mainObject = new T();
            List<T1> listData = new List<T1>();
            var header = arr[1];

            for (int i = 0; i < header.Count; i++) {
                if (header[i] != string.Empty) {
                    var property = type.GetField(header[i].Replace("\r", string.Empty));
                    if (property != null) {
                        if (arr.Count > 2 && arr[2] != null) {
                            var value = arr[2][i].Replace("\r", string.Empty);
                            var x = property.FieldType;
                            if (x.IsGenericType) {
                                var currentList = GetSubTable(arr, i);
                                listData = FillDataIsList<T1>(currentList);
                            } else if (x.IsEnum) {
                                object objValue = Enum.Parse(property.FieldType, value);
                                property.SetValue(mainObject, objValue);
                            } else if (String.IsNullOrEmpty(value)) {

                            } else {
                                object objValue = Convert.ChangeType(value, property.FieldType);
                                property.SetValue(mainObject, objValue);
                            }
                        }
                    }
                }
            }

            foreach (FieldInfo fieldInfo in type.GetFields()) {
                if (fieldInfo.FieldType.IsGenericType) {
                    var a = fieldInfo.FieldType.GetGenericArguments()[0];

                    if (a == type1) {
                        object objValue = Convert.ChangeType(listData, fieldInfo.FieldType);
                        fieldInfo.SetValue(mainObject, objValue);
                    }
                }
            }

            return mainObject;
        }

        public static T FillDataHasList<T, T1, T2>(string id, string gridId) where T : new() where T1 : new() where T2 : new() {
            List<List<string>> arr = new List<List<string>>();

            GetTable(id, gridId, list => arr = list);

            var type = typeof(T);
            var type1 = typeof(T1);
            var type2 = typeof(T2);
            T mainObject = new T();
            List<T1> listData = new List<T1>();
            var header = arr[1];

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < header.Count; i++) {
                if (header[i] != string.Empty) {
                    var property = type.GetField(header[i].Replace("\r", string.Empty));
                    if (property != null) {
                        if (arr.Count > 2 && arr[2] != null) {
                            var value = arr[2][i].Replace("\r", string.Empty);
                            var x = property.FieldType;
                            if (x.IsGenericType) {
                                var currentList = GetSubTable(arr, i);
                                listData.Add(FillDataHasList<T1, T2>("", "", currentList));
                            } else if (x.IsEnum) {
                                object objValue = Enum.Parse(property.FieldType, value);
                                property.SetValue(mainObject, objValue);
                            } else if (String.IsNullOrEmpty(value)) {

                            } else {
                                object objValue = Convert.ChangeType(value, property.FieldType);
                                property.SetValue(mainObject, objValue);
                            }
                        }
                    }
                }
            }

            foreach (FieldInfo fieldInfo in type.GetFields()) {
                if (fieldInfo.FieldType.IsGenericType) {
                    var a = fieldInfo.FieldType.GetGenericArguments()[0];

                    if (a == type1) {
                        object objValue = Convert.ChangeType(listData, fieldInfo.FieldType);
                        fieldInfo.SetValue(mainObject, objValue);
                    }
                }
            }

            return mainObject;
        }

        private static List<List<string>> GetSubTable(List<List<string>> mainTable, int index) {
            List<List<string>> result = new List<List<string>>();
            mainTable.ForEach(list => result.Add(new List<string>(list)));
            int curreinIndex = index;

            int next = -1;
            for (int k = index + 1; k < result[1].Count; k++) {
                if (result[1][k] != string.Empty) {
                    next = k;
                    break;
                }
            }

            result.ForEach(list => {
                if (next != -1 && list.Count - next + 1 > 0)
                    list.RemoveRange(next, list.Count - next);
                list.RemoveRange(0, curreinIndex);
            });
            result.RemoveAt(0);

            StringBuilder sb = new StringBuilder();
            result.ForEach(list => {
                list.ForEach(str => sb.Append(str + " | "));
                sb.Append("\n");
            });

            return result;
        }

        public static void GetTable(string id, string gridId, Action<List<List<string>>> callBack) {
            LoadWebClient(id, gridId, s => {
                var listStr = GetTable(s);
                callBack(listStr);
            });
        }

        /// <summary>
        /// Get sub-table from list, remove first row 
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        private static List<List<string>> GetTable(List<List<string>> listStr) {
            List<List<string>> newTable = new List<List<string>>();
            for (int i = 1; i < listStr.Count; i++) {
                if (listStr[i][0] == string.Empty)
                    break;

                newTable.Add(listStr[i]);
            }

            return newTable;
        }

        /// <summary>
        /// Get table from string, load from data fiel
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        static List<List<string>> GetTable(string allData) {
            List<List<string>> listStr = new List<List<string>>();
            var arr = allData.Split('\n');
            for (int i = 0; i < arr.Length; i++) {
                var subArr = arr[i].Split(',');
                if (subArr[0] == string.Empty)
                    break;

                List<string> row = new List<string>();
                row.AddRange(subArr);
                listStr.Add(row);
            }

            return listStr;
        }

        public static void LoadWebClient(string id, string gridId, Action<string> callBack) {
            string csvFile = string.Format(
                @"https://docs.google.com/spreadsheet/ccc?key={0}&usp=sharing&output=csv&id=KEY&gid={1}", id, gridId);
            LoadWebClient3(csvFile, callBack);
        }

        public static void LoadWebClient3(string id, Action<string> callBack) {
            WWW w = new WWW(id);

            while (!w.isDone) {
                w.MoveNext();
            }

            callBack(w.text);
            Debug.Log(w.text);
        }
    }
}
