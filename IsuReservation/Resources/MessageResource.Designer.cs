﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IsuReservation.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class MessageResource {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MessageResource() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("IsuReservation.Resources.MessageResource", typeof(MessageResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string NameFieldEmpty {
            get {
                return ResourceManager.GetString("NameFieldEmpty", resourceCulture);
            }
        }
        
        internal static string BirthDayFieldEmpty {
            get {
                return ResourceManager.GetString("BirthDayFieldEmpty", resourceCulture);
            }
        }
        
        internal static string ContactTypeFieldEmpty {
            get {
                return ResourceManager.GetString("ContactTypeFieldEmpty", resourceCulture);
            }
        }
        
        internal static string AgeOlderThan18 {
            get {
                return ResourceManager.GetString("AgeOlderThan18", resourceCulture);
            }
        }
        
        internal static string InvalidDate {
            get {
                return ResourceManager.GetString("InvalidDate", resourceCulture);
            }
        }
        
        internal static string ContactNotFound {
            get {
                return ResourceManager.GetString("ContactNotFound", resourceCulture);
            }
        }
        
        internal static string ContactAlreadyExist {
            get {
                return ResourceManager.GetString("ContactAlreadyExist", resourceCulture);
            }
        }
    }
}