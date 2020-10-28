var table = null;

$(document).ready(function () {
    //debugger;
    table = $('#myTable').DataTable({
        "oLanguage": {
            "sSearch": "Filter Data"},
        "iDisplayLength": -1,
        "sPaginationType": "full_numbers",
        "ajax": {
            url: "/Departments/LoadDepartment",
            type: "GET",
            dataType: "json",
            dataSrc: ""},
        "columns": [
            {
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }},
            { "data": "Name" },
            {
                "data": "dateTime",
                "render": function (data) {
                    return getDateString(data);
                }
            },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.Id + ')"><i class="fa fa-edit"></i></button>' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.Id + ')"><i class="fa fa-trash"></i></button>'
                }
            }]
    });

    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
            var min = $('#min').datepicker("getDate");
            var max = $('#max').datepicker("getDate");
            var startDate = new Date(data[2]);
            //debugger;
            if (min == null && max == null) { return true; }
            if (min == null && startDate <= max) { return true; }
            if (max == null && startDate >= min) { return true; }
            if (startDate <= max && startDate >= min) { return true; }
            return false;
        }
    );

    $("#min").datepicker({ onSelect: function () { table.draw(); }, changeMonth: true, changeYear: true });
    $("#max").datepicker({ onSelect: function () { table.draw(); }, changeMonth: true, changeYear: true });

    $('#min, #max').change(function () {
        table.draw();
    });
});



function getDateString(date) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(date);
    var dt = new Date(parseFloat(results[1]));
    return dt.getFullYear() + "/" + (dt.getMonth() + 1) + "/" + dt.getDate();
}

function Save() {
    //debugger;
    var Department = new Object();
    Department.Name = $('#Name').val();
    Department.dateTime = $('#Date').val();
    $.ajax({
        type: 'POST',
        url: '/Departments/Insert',
        data: Department
    }).then((result) => {
        if (Department.Name != "") {
            //debugger;
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
    //debugger;
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
            $('#Date').val(obj.dateTime);
            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
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
    Department.dateTime = $('#Date').val();
    $.ajax({
        type: "POST",
        url: '/Departments/Update/',
        data: Department
    }).then((result) => {
        if (Department.Name != "") {
            //debugger;
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
            //debugger;
            $.ajax({
                url: "/Departments/Delete/",
                data: { Id: Id }
            }).then((result) => {
                //debugger;
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
    $('#Date').val('');
    $('#Update').hide();
    $('#Save').show();
}