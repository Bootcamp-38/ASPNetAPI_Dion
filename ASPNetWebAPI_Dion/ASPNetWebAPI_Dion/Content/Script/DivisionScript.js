var table = null;

$(document).ready(function () {
    debugger;
    table = $('#myTable').DataTable({
        "processing": true,
        "ajax": {
            url: "/Divisions/LoadDivision",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "Name" },
            { "data": "Dept_Name"},
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.Id + ')"><i class="fa fa-edit"></i></button>' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false"  title="Delete" onclick="return Delete(' + row.Id + ')"><i class="fa fa-trash"></i></button>'
                }
            }]
    });
});

function GetById(Id) {
    debugger;
    $.ajax({
        url: "/Divisions/GetById/" + Id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            const obj = JSON.parse(result);
            $('#Id').val(obj.Id);
            $('#Name').val(obj.Name);
            $('#Department').val(obj.Dept_Id);
            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
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
                url: "/Divisions/Delete/",
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

var Departments = []
function LoadDepartment(element) {
    debugger;
    if (Departments.length == 0) {
        $.ajax({
            type: "Get",
            url: "/Departments/LoadDepartment",
            success: function (data) {
                debugger;
                Departments = data;
                renderDepartment(element);
            }
        })
    }
    else {
        renderDepartment(element);
    }
}

function renderDepartment(element) {
    debugger;
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select Department').hide());
    $.each(Departments, function (i, val) {
        debugger;
        $ele.append($('<option/>').val(val.Id).text(val.Name));
    })
}
LoadDepartment($('#Department'));

function Save() {
    debugger;
    var Division = new Object();
    Division.Name = $('#Name').val();
    Division.Dept_Id = $('#Department').val();
    $.ajax({
        type: 'POST',
        url: '/Divisions/Insert',
        data: Division
    }).then((result) => {
        if (Division.Name != "") {
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

function Update() {
    debugger;
    var Division = new Object();
    Division.Id = $('#Id').val();
    Division.Name = $('#Name').val();
    Division.Dept_Id = $('#Department').val();
    $.ajax({
        type: "POST",
        url: '/Divisions/Update/',
        data: Division
    }).then((result) => {
        debugger;
        if (result.StatusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Division Updated Successfully'
            });
            table.ajax.reload();
        } else {
            Swal.fire('Error', 'Failed to Update', 'error');
            ClearScreen();
        }
    })
}

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#Update').hide();
    $('#Save').show();
}
