using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB
{
    public class ObjectHelper
    {
        public class PropertyCopier<TParent, TChild>
            where TParent : class
            where TChild : class
        {
            /// <summary>
            /// copy same properties from an object to another
            /// </summary>
            /// <param name="parent"></param>
            /// <param name="child"></param>
            public static void Copy(TParent parent, ref TChild child)
            {
                System.Reflection.PropertyInfo[] parentProperties = parent.GetType().GetProperties();
                System.Reflection.PropertyInfo[] childProperties = child.GetType().GetProperties();

                foreach (System.Reflection.PropertyInfo childProperty in childProperties)
                {
                    if (childProperty.GetSetMethod() == null) { continue; }
                    foreach (System.Reflection.PropertyInfo parentProperty in parentProperties)
                    {
                        if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                        {
                            object parentVal = parentProperty.GetValue(parent);
                            if (parentVal == null)
                            {
                                break;
                            }
                            object childVal = childProperty.GetValue(child);
                            childProperty.SetValue(child, parentVal);
                            break;
                        }
                    }
                }
            }

            /// <summary>
            /// copy matched properties from an object to another
            /// </summary>
            /// <param name="parent"></param>
            /// <param name="child"></param>
            public static void GenerateMatchedObject(TParent parent, ref TChild child)
            {
                System.Reflection.PropertyInfo[] childProperties = child.GetType().GetProperties();
                System.Reflection.PropertyInfo[] parentProperties = parent.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo childProperty in childProperties)
                {
                    if (childProperty.GetSetMethod() == null) { continue; }
                    object[] attributesForProperty = childProperty.GetCustomAttributes(typeof(MatchParentAttribute), true);
                    bool isOfTypeMatchParentAttribute = false;

                    MatchParentAttribute currentAttribute = null;

                    foreach (object attribute in attributesForProperty)
                    {
                        if (attribute.GetType() == typeof(MatchParentAttribute))
                        {
                            isOfTypeMatchParentAttribute = true;
                            currentAttribute = (MatchParentAttribute)attribute;
                            break;
                        }
                    }
                    bool isParentHasAttribute = parentProperties.Select(a => a.Name).ToList().Contains(currentAttribute?.ParentPropertyName);

                    if (isOfTypeMatchParentAttribute && isParentHasAttribute)
                    {
                        object parentPropertyValue = null;
                        foreach (System.Reflection.PropertyInfo parentProperty in parentProperties)
                        {
                            if (parentProperty.Name == currentAttribute.ParentPropertyName)
                            {
                                if (parentProperty.PropertyType == childProperty.PropertyType)
                                {
                                    parentPropertyValue = parentProperty.GetValue(parent);
                                }
                                childProperty.SetValue(child, parentPropertyValue);
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (System.Reflection.PropertyInfo parentProperty in parentProperties)
                        {
                            if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                            {
                                object parentVal = parentProperty.GetValue(parent);
                                if (parentVal == null)
                                {
                                    break;
                                }
                                childProperty.SetValue(child, parentVal);
                                break;
                            }
                        }
                    }
                }
            }
        }
        public class PropertyCopierReturn<TParent, TChild>
           where TParent : class
           where TChild : class, new()
        {
            /// <summary>
            /// Return child object with properties copied from parent object
            /// </summary>
            /// <param name="parent"></param>
            /// <param name="child"></param>
            public static TChild Copy(TParent parent)
            {
                TChild child = new TChild();
                PropertyCopier<TParent, TChild>.Copy(parent, ref child);
                return child;
            }

            /// <summary>
            /// Return child object with matched properties copied from parent object
            /// </summary>
            /// <param name="parent"></param>
            /// <param name="child"></param>
            public static TChild GenerateMatchedObject(TParent parent)
            {
                TChild child = new TChild();
                PropertyCopier<TParent, TChild>.GenerateMatchedObject(parent, ref child);
                return child;
            }
        }
    }

    #region Copy Matched Properties
    [AttributeUsage(AttributeTargets.Property)]
    public class MatchParentAttribute : Attribute
    {
        public readonly string ParentPropertyName;
        public readonly string[] ParentPropertyNameArray;
        public MatchParentAttribute(string parentPropertyName)
        {
            ParentPropertyName = parentPropertyName;
        }
        public MatchParentAttribute(params string[] parentPropertyName)
        {
            ParentPropertyNameArray = parentPropertyName;
        }
    }
    #endregion

    #region extendObject
    public static class ObjectExtensionMethods
    {
        /// <summary>
        /// Copy giá trị của các Property có cùng tên và kiểu dữ liệu
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        public static void CopyPropertiesFrom(this object self, object parent)
        {
            System.Reflection.PropertyInfo[] fromProperties = parent.GetType().GetProperties();
            System.Reflection.PropertyInfo[] toProperties = self.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo fromProperty in fromProperties)
            {
                foreach (System.Reflection.PropertyInfo toProperty in toProperties)
                {
                    if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        object parentVal = fromProperty.GetValue(parent);
                        if (parentVal == null)
                        {
                            break;
                        }
                        toProperty.SetValue(self, parentVal);
                        break;
                    }
                }
            }
        }

        public static string Dump(this object self)
        {
            System.Reflection.PropertyInfo[] fromProperties = self.GetType().GetProperties();

            string dump = "";

            foreach (System.Reflection.PropertyInfo fromProperty in fromProperties)
            {
                object value = fromProperty.GetValue(self);
                dump += $"{fromProperty}: {value} \n";
            }
            return dump;
        }


        public static void CopyPropertiesFrom(this object self, object parent, string prefix, string subfix)
        {
            if (self == null || parent == null) { return; }

            System.Reflection.PropertyInfo[] fromProperties = parent.GetType().GetProperties();
            System.Reflection.PropertyInfo[] toProperties = self.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo fromProperty in fromProperties)
            {
                string fromFixed = $"{prefix}{fromProperty.Name}{subfix}";
                foreach (System.Reflection.PropertyInfo toProperty in toProperties)
                {
                    if (fromFixed == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        object parentVal = fromProperty.GetValue(parent);
                        toProperty.SetValue(self, parentVal);
                        break;
                    }
                }
            }
        }

        public static T CopyPropertiesTo<T>(this object self) where T : class, new()
        {
            T child = new T();
            child.CopyPropertiesFrom(self);
            return child;
        }

        public static void MatchPropertiesFrom(this object self, object parent)
        {
            System.Reflection.PropertyInfo[] childProperties = self.GetType().GetProperties();
            System.Reflection.PropertyInfo[] parentProperties = parent.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo childProperty in childProperties)
            {
                if (childProperty.GetSetMethod() == null) { continue; }
                object[] attributesForProperty = childProperty.GetCustomAttributes(typeof(MatchParentAttribute), true);
                bool isOfTypeMatchParentAttribute = false;

                MatchParentAttribute currentAttribute = null;

                foreach (object attribute in attributesForProperty)
                {
                    if (attribute.GetType() == typeof(MatchParentAttribute))
                    {
                        isOfTypeMatchParentAttribute = true;
                        currentAttribute = (MatchParentAttribute)attribute;
                        break;
                    }
                }
                bool isParentHasAttribute = parentProperties.Select(a => a.Name).ToList().Contains(currentAttribute?.ParentPropertyName);

                if (isOfTypeMatchParentAttribute && isParentHasAttribute)
                {
                    object parentPropertyValue = null;
                    foreach (System.Reflection.PropertyInfo parentProperty in parentProperties)
                    {
                        if (parentProperty.Name == currentAttribute.ParentPropertyName)
                        {
                            if (parentProperty.PropertyType == childProperty.PropertyType)
                            {
                                parentPropertyValue = parentProperty.GetValue(parent);
                            }
                        }
                    }
                    childProperty.SetValue(self, parentPropertyValue);
                }
                else
                {
                    foreach (System.Reflection.PropertyInfo parentProperty in parentProperties)
                    {
                        if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                        {
                            object parentVal = parentProperty.GetValue(parent);
                            if (parentVal == null)
                            {
                                break;
                            }
                            childProperty.SetValue(self, parentVal);
                            break;
                        }
                    }
                }
            }
        }

        public static T CopyNonNullProperty<T>(this object self, T other) where T : class, new()
        {
            System.Reflection.PropertyInfo[] fromProperties = self.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo fromProperty in fromProperties)
            {
                object value = fromProperty.GetValue(self);

                if (value != null)
                {
                    other.GetType().GetProperty(fromProperty.Name)?.SetValue(other, value);
                }
            }

            return other;
        }

        public static T MatchPropertiesTo<T>(this object self) where T : class, new()
        {
            T child = new T();
            child.MatchPropertiesFrom(self);
            return child;
        }

        public static string AssignIfNullOrEmpty(this string Value, string AssignValue)
        {
            return string.IsNullOrEmpty(Value) ? AssignValue : Value;
        }

        public static object GetPropertyValue(this object obj, string propName) 
        {
            return obj.GetType().GetProperty(propName).GetValue(obj, null);
        }
    }
    #endregion
}
