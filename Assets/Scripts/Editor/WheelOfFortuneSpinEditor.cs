using UnityEditor;
using UnityEngine;
using WheelOfFortune.Spin;

[CustomEditor(typeof(WheelOfFortuneSpin))]
public class WheelOfFortuneSpinEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get the SerializedProperty for the target segment and segments list
        SerializedProperty targetSegmentProp = serializedObject.FindProperty("_targetSegment");
        SerializedProperty segmentsProp = serializedObject.FindProperty("_segments");

        // Draw the default inspector for other properties
        DrawDefaultInspector();

        // Create a list of segment labels for the dropdown
        string[] segmentLabels = new string[segmentsProp.arraySize];
        for (int i = 0; i < segmentsProp.arraySize; i++)
        {
            // Fetch segment label from the current element in the array
            SerializedProperty segmentProp = segmentsProp.GetArrayElementAtIndex(i);
            segmentLabels[i] = segmentProp.FindPropertyRelative("Label").stringValue;
        }

        // Create the dropdown with the segment labels
        int selectedIndex = Mathf.Max(0, System.Array.IndexOf(segmentLabels, targetSegmentProp.stringValue));
        int newSelectedIndex = EditorGUILayout.Popup("Target Segment", selectedIndex, segmentLabels);

        // If selection changes, update the target segment and mark as dirty
        if (newSelectedIndex != selectedIndex)
        {
            targetSegmentProp.stringValue = segmentLabels[newSelectedIndex];
            serializedObject.ApplyModifiedProperties(); // Apply changes to serialized object
        }

        // Add a button below the dropdown that will trigger the spin
        if (GUILayout.Button("Spin Wheel"))
        {
            // Access the WheelOfFortuneSpin component and call the SpinWheel method
            WheelOfFortuneSpin wheelOfFortuneSpin = (WheelOfFortuneSpin)target;
            wheelOfFortuneSpin.SpinWheel();
        }
    }
}
