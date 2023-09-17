# Documentation
1. If you have a .json file (JSON to Hierarchy)
   - put it in the "Assets" folder of the project(Example file is already present in the folder).
   - then select the "Hierarchy Manager" object in the scene and fill the "File Path" field with the name of the json file along with the prefix "/" and suffix ".json".
   - then press the "LOAD" button which generates the Hierarchy

2. If you have a Hierarchy (Hierarchy to JSON)
   - Select the "Hierarchy Manager" -> drag and drop the Hierarchy parent to "Parent Object" field.
   - then press the "READ FROM HIERARCHY" button and then the "SAVE" to generate the json file.
   - you can change the file path to chnage where to save the generated json file.

3. Editing data
   - you can edit the objects from scene and press "READ FROM HIERARCHY" button to make all the changes to the objects
   - or you can edit in the inspector itself and use "READ FROM INSPECTOR" button to copy the changes
   - Make sure to SAVE to apply changes both to the json file and to the scene objects.
  
NOTE: For the sake of ease, information of buttons are added in thier tool tips.
