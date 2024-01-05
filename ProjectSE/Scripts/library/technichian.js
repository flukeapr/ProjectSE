const { Modal } = require("bootstrap");

 $(document).ready(function () {
    
    
});
 var isUpdateable = false;
 
    

 function getTechnicainById(id) {
    $("#title").text("แก้ไขสมาชิกช่าง");
     $.ajax({
        url: '/Admin/Get/' + id,
         type: 'GET',
         dataType: 'json',
         success: function(data) {
             $("#ID").val(data.technician_Id);
             $("#Name").val(data.technicianName);
             $("#Type").val(data.typeRepair);
             $("#Phone").val(data.phone);
             $("#Image").val(data.image);
             isUpdateable = true;
             $("#empModal").modal('show');
            
        },
         error: function(err) {
             alert("Error: " + err.responseText);
            
        } 
 });
    
}



 $("#btnSave").click(function (e) {
     var data = {
         ID: $("#ID").val(),
         Name: $("#Name").val(),
         Position: $("#Position").val(),
         Address: $("#Address").val() 
}
     if (!isUpdateable) {
         $.ajax({
             url: '/Employees/Create/',
             type: 'POST',
             dataType: 'json',
            data: data,
            success: function(data) {
                 getEmployees();
                 $("#empModal").modal('hide');
                 clear();
                
            },
             error: function(err) {
                 alert("Error: " + err.responseText);
                
            } 
 })
        
    }
     else {
         $.ajax({
             url: '/Employees/Update/',
             type: 'POST',
             dataType: 'json',
             data: data,
             success: function(data) {
                 getEmployees();
                 isUpdateable = false;
                 $("#empModal").modal('hide');
                 clear();
                
            },
             error: function(err) {
                 alert("Error: " + err.responseText);
                
            } 
 })
        
    }
    
});
 function confirmdeleteTechnicianById(id) {
     $("#confirmModal #title").text("ลบสมาชิกช่าง");

     $("#confirmModal").modal('show');
        
         $("#confirmModal #btnOK").click(function (e) {
             $.ajax({
                 url: "/Admin/Delete/" + id,
                 type: "POST",
                 dataType: 'json',
                 success: function (data) {

                     $("#confirmModal").modal('hide');

                 },
                 error: function (err) {
                     alert("Error: " + err.responseText);

                 }
             });
             e.preventDefault();

         });
  
}
 $("#btnCreate").click(function () {
     $("#title").text("Create New");
    
})
 $("#btnClose").click(function () {
     clear();
    
});
 function clear() {
     $("#ID").val("");
     $("#Name").val("");
     $("#Position").val("");
     $("#Address").val("");
    
}
