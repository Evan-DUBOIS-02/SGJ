using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ChoiceCondition))]
public class ChoiceConditionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var requestDataProp = property.FindPropertyRelative("requestData");
        var choiceProp = property.FindPropertyRelative("choice");

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = 4f;

        // Ligne 1 : RequestData field
        Rect requestRect = new Rect(position.x, position.y, position.width, lineHeight);
        EditorGUI.PropertyField(requestRect, requestDataProp);

        // Ligne 2 : Dropdown des choix
        Rect choiceRect = new Rect(position.x, position.y + lineHeight + spacing, position.width, lineHeight);

        if (requestDataProp.objectReferenceValue != null)
        {
            var requestData = requestDataProp.objectReferenceValue as RequestData;

            if (requestData != null && requestData.Choices != null && requestData.Choices.Count > 0)
            {
                string[] options = new string[requestData.Choices.Count];

                for (int i = 0; i < options.Length; i++)
                {
                    options[i] = requestData.Choices[i].ChoiceText;
                }

                choiceProp.intValue = EditorGUI.Popup(choiceRect, "Choice", choiceProp.intValue, options);
            }
            else
            {
                EditorGUI.LabelField(choiceRect, "No choices available");
            }
        }
        else
        {
            EditorGUI.LabelField(choiceRect, "Select a RequestData first");
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2 + 4f;
    }
}