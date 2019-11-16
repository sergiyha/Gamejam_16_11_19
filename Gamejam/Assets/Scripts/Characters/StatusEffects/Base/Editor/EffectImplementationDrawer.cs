using System.Linq;
using System.Reflection;
using Characters.StatusEffects;
using UnityEditor;
using UnityEngine;

namespace Editor
{

    [CustomPropertyDrawer(typeof(EffectImplementationAttribute), true)]
    public class EffectImplementationDrawer : PropertyDrawer
    {
        private static string[] allEffects;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (allEffects == null)
            {
                string nspace = typeof(Bleed).Namespace;
                
                var q = (from t in Assembly.GetAssembly(typeof(Bleed)).GetTypes()
                    where t.IsClass && t.Namespace != null && t.Namespace.Contains(nspace)
                    select t).ToList();

                for (int i = 0; i < q.Count; i++)
                {
                    var baseT = q[i].BaseType;
                    bool quit = false;
                    while (!quit)
                    {
                        if (baseT == null)
                        {
                            q.RemoveAt(i);
                            i--;
                            quit = true;
                        }
                        else if(baseT == typeof(StatusEffectBase))
                            quit = true;
                        else
                            baseT = baseT.BaseType;
                    } 
                }

                allEffects = q.Select(t=>t.Name).ToArray();
                if (allEffects.Length == 0)
                {
                    Debug.LogError("NO EFFECTS FOUND");
                    allEffects = new string[2]
                    {
                        "!!!NO EFFECT FOUND!!!",
                        "KTOTO TUT TUPANOL, AND IT WASN'T YA"
                    };
                }
            }

            label.text = property.displayName;
            EditorGUI.BeginProperty(position, label, property);
            
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
          
            property.stringValue = ((BaseEffectData)property.serializedObject.targetObject).GetType().Name.Replace("EffectData", string.Empty);
           
            EditorGUI.LabelField(position, property.stringValue);
            EditorGUI.EndProperty();
        }
    }
}
