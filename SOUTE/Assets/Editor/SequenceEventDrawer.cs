using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(SequenceEvent))]
public class SequenceEventDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float indent = EditorGUI.IndentedRect(position).x - position.x;
        position.x += indent;
        position.width -= indent;

        property.isExpanded = EditorGUI.Foldout(
            new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
            property.isExpanded,
            label
        );

        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;

            Rect orderPosition = new Rect(
                position.x,
                position.y + EditorGUIUtility.singleLineHeight,
                position.width,
                EditorGUIUtility.singleLineHeight
            );

            SerializedProperty order = property.FindPropertyRelative("order");
            EditorGUI.PropertyField(orderPosition, order);

            SOUTESOrdering orderType = (SOUTESOrdering)order.enumValueIndex;

            float yOffset = EditorGUIUtility.singleLineHeight * 2;

            if (orderType != SOUTESOrdering.Wait)
            {
                Rect SOUTEPosition = new Rect(
                    position.x,
                    position.y + yOffset,
                    position.width,
                    EditorGUIUtility.singleLineHeight);

                SerializedProperty SOUTE = property.FindPropertyRelative("SOUTE");
                EditorGUI.PropertyField(SOUTEPosition, SOUTE, new GUIContent("Event"));

                yOffset += EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;

                GameEvent SOUTEReference = (GameEvent)SOUTE?.objectReferenceValue;
                bool setFloatActive = SOUTEReference == null ? false : SOUTEReference.setFloat;

                if (setFloatActive)
                {
                    Rect setFloatPosition = new Rect(
                        position.x,
                        position.y + yOffset,
                        position.width,
                        EditorGUIUtility.singleLineHeight);

                    SerializedProperty setFloat = property.FindPropertyRelative("setFloat");
                    EditorGUI.PropertyField(setFloatPosition, setFloat, new GUIContent("Float Input"));

                    yOffset += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                    Rect referenceFloatPosition = new Rect(
                        position.x,
                        position.y + yOffset,
                        position.width,
                        EditorGUIUtility.singleLineHeight);

                    SerializedProperty referenceFloat = property.FindPropertyRelative("setFloatReference");
                    EditorGUI.PropertyField(referenceFloatPosition, referenceFloat, new GUIContent("Float Reference"));
                }
            }
            else
            {
                Rect durationPosition = new Rect(
                    position.x,
                    position.y + yOffset,
                    position.width,
                    EditorGUIUtility.singleLineHeight);

                SerializedProperty duration = property.FindPropertyRelative("duration");
                EditorGUI.PropertyField(durationPosition, duration, new GUIContent("Duration (sec)"));
            }

            EditorGUI.indentLevel--;

            EditorGUI.EndProperty();
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!property.isExpanded)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        SerializedProperty order = property.FindPropertyRelative("order");
        SOUTESOrdering orderType = (SOUTESOrdering)order.enumValueIndex;

        float height = EditorGUIUtility.singleLineHeight;

        height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        if (orderType != SOUTESOrdering.Wait)
        {
            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            SerializedProperty SOUTE = property?.FindPropertyRelative("SOUTE");
            GameEvent SOUTEReference = (GameEvent)SOUTE?.objectReferenceValue;
            bool setFloatActive = SOUTEReference == null ? false : SOUTEReference.setFloat;

            if (setFloatActive)
            {
                height += (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2 + EditorGUIUtility.singleLineHeight;
            }
        }
        else
        {
            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        return height;
    }
}