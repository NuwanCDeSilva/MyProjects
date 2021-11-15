
//------------------------- This JavaScript method for TreeView checkbox selection --------------------------------// 
        var TREEVIEW_ID = "tViewGrantSystemOption"; //the ID of the TreeView control
                                                    //the constants used by GetNodeIndex()
        var LINK = 0;
        var CHECKBOX = 1;
        
        //this function is executed whenever user clicks on the node text
        function ToggleCheckBox(senderId)
        {
            var nodeIndex = GetNodeIndex(senderId, LINK);
            var checkBoxId = TREEVIEW_ID + "n" + nodeIndex + "CheckBox";
            var checkBox = document.getElementById(checkBoxId);
            checkBox.checked = !checkBox.checked;
            
            ToggleChildCheckBoxes(checkBox);
            //ToggleParentCheckBox(checkBox);
        }
        
        //checkbox click event handler
        function checkBox_Click(eventElement)
        {
            ToggleChildCheckBoxes(eventElement.target);
            //ToggleParentCheckBox(eventElement.target);
        }
        
        //returns the index of the clicked link or the checkbox
        function GetNodeIndex(elementId, elementType)
        {
             var nodeIndex;
             if(elementType == LINK)
             {
                nodeIndex = elementId.substring((TREEVIEW_ID + "t").length);
             }
             else if (elementType == CHECKBOX)
             {
                nodeIndex = elementId.substring((TREEVIEW_ID + "n").length, elementId.indexOf("CheckBox"));
             }
             return nodeIndex;
        }
        
        //checks or unchecks the nested checkboxes
        function ToggleChildCheckBoxes(checkBox)
        {
            var postfix = "n";
            var childContainerId = TREEVIEW_ID + postfix + GetNodeIndex(checkBox.id, CHECKBOX) + "Nodes";
            var childContainer = document.getElementById(childContainerId);
            if (childContainer)
            {
                var childCheckBoxes = childContainer.getElementsByTagName("input");
                for (var i = 0; i < childCheckBoxes.length; i++)
                {
                    childCheckBoxes[i].checked = checkBox.checked;
                }
            }
        }
        
        //unchecks the parent checkboxes if the current one is unchecked
//        function ToggleParentCheckBox(checkBox)
//        {
//            if(checkBox.checked == false)
//            {
//                var parentContainer = GetParentNodeById(checkBox, TREEVIEW_ID);
//                if(parentContainer) 
//                {
//                    var parentCheckBoxId = parentContainer.id.substring(0, parentContainer.id.search("Nodes")) + "CheckBox";
//                    if($get(parentCheckBoxId) && $get(parentCheckBoxId).type == "checkbox") 
//                    {
//                        $get(parentCheckBoxId).checked = false;
//                        ToggleParentCheckBox($get(parentCheckBoxId));
//                    }
//                }
//            }
//        }
        
        //returns the ID of the parent container if the current checkbox is unchecked
        function GetParentNodeById(element, id)
        {
            var parent = element.parentNode;
            if (parent == null)
            {
                return false;
            }
            if (parent.id.search(id) == -1)
            {
                return GetParentNodeById(parent, id);
            }
            else
            {
                return parent;
            }
        }