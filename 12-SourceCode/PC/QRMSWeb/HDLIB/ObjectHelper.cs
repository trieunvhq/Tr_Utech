﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB
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
                var parentProperties = parent.GetType().GetProperties();
                var childProperties = child.GetType().GetProperties();

                foreach (var childProperty in childProperties)
                {
                    if (childProperty.GetSetMethod() == null) { continue; }
                    foreach (var parentProperty in parentProperties)
                    {
                        if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                        {
                            var parentVal = parentProperty.GetValue(parent);
                            if (parentVal == null)
                            {
                                break;
                            }
                            var childVal = childProperty.GetValue(child);
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
                var childProperties = child.GetType().GetProperties();
                var parentProperties = parent.GetType().GetProperties();
                foreach (var childProperty in childProperties)
                {
                    if (childProperty.GetSetMethod() == null) { continue; }
                    var attributesForProperty = childProperty.GetCustomAttributes(typeof(MatchParentAttribute), true);
                    var isOfTypeMatchParentAttribute = false;

                    MatchParentAttribute currentAttribute = null;

                    foreach (var attribute in attributesForProperty)
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
                        foreach (var parentProperty in parentProperties)
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
                        foreach (var parentProperty in parentProperties)
                        {
                            if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                            {
                                var parentVal = parentProperty.GetValue(parent);
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
                PropertyCopier<TParent, TChild>.GenerateMatchedObject(parent,ref child);
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
            var fromProperties = parent.GetType().GetProperties();
            var toProperties = self.GetType().GetProperties();

            foreach (var fromProperty in fromProperties)
            {
                foreach (var toProperty in toProperties)
                {
                    if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        var parentVal = fromProperty.GetValue(parent);
                        if (parentVal == null)
                        {
                            break;
                        }
                        toProperty.SetValue(self,parentVal);
                        break;
                    }
                }
            }
        }

        public static void MatchPropertiesFrom(this object self, object parent)
        {
            var childProperties = self.GetType().GetProperties();
            var parentProperties = parent.GetType().GetProperties();
            foreach (var childProperty in childProperties)
            {
                if (childProperty.GetSetMethod() == null) { continue; }
                var attributesForProperty = childProperty.GetCustomAttributes(typeof(MatchParentAttribute), true);
                var isOfTypeMatchParentAttribute = false;

                MatchParentAttribute currentAttribute = null;

                foreach (var attribute in attributesForProperty)
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
                    foreach (var parentProperty in parentProperties)
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
                    foreach (var parentProperty in parentProperties)
                    {
                        if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                        {
                            var parentVal = parentProperty.GetValue(parent);
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
        
        public static string AssignIfNullOrEmpty(this string Value, string AssignValue)
        {
            return string.IsNullOrEmpty(Value) ? AssignValue : Value;
        }
    }
    #endregion
}
