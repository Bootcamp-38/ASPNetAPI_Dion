//import { Swal } from "./sweetalert2@10.";
//import { result } from "lodash";

var table = null;

$(document).ready(function () {
    debugger;
    table = $('#myTable').DataTable({
        "processing": true,
        "ajax": {
            url: "/Departments/LoadDepartment",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }},
            { "data": "Name" },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.Id + ')">Edit</button>' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.Id + ')">Delete</button>'
                }
            }]
    });
});

function Save() {
    debugger;
    var Department = new Object();
    Department.Name = $('#Name').val();
    $.ajax({
        type: 'POST',
        url: '/Departments/Insert',
        data: Department
    }).then((result) => {
        if (Department.Name != "") {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Department Added Successfully'
                });
                table.ajax.reload();
            } else {
                Swal.fire('Error', 'Failed to Input', 'error');
                ClearScreen();
                table.ajax.reload();
            }
        }
        else {
            Swal.fire('Error', 'Please Insert Department Name', 'error');
        }
    })
}

function GetById(Id) {
    debugger;
    $.ajax({
        url: "/Departments/GetById/" + Id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            const obj = JSON.parse(result);
            $('#Id').val(obj.Id);
            $('#Name').val(obj.Name);
            $('#myModal').modal('show');
            $('#Update').show();
            $('#Update').show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Update() {
    //debugger;
    var Department = new Object();
    Department.Id = $('#Id').val();
    Department.Name = $('#Name').val();
    $.ajax({
        type: "POST",
        url: '/Departments/Update/',
        data: Department
    }).then((result) => {
        if (Department.Name != "") {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Department Updated Successfully'
                });
                table.ajax.reload();
            } else {
                Swal.fire('Error', 'Failed to Update', 'error');
                ClearScreen();
            }
        }
        else {
            Swal.fire('Error', 'Please Update Department Name', 'error');
        }
    })
}

function Delete(Id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonTet: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            debugger;
            $.ajax({
                url: "/Departments/Delete/",
                data: { Id: Id }
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire({
                        position: 'center',
                        type: 'success',
                        title: 'Delete Successfully'
                    });
                    table.ajax.reload();
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            })
        };
    });
}

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#Update').hide();
    $('#Save').show();
}