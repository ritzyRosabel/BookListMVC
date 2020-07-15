var dataTable;

$(document).ready({
    LoadDataTable();
});

function LoadDataTable() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/books/getall",
            "type": "GET",
            "datatype":"json"

        },
        "columns": [
            {
                "data": "name", "width": "20%";
                "data": "author", "width": "20%";
                "data": "isbn", "width": "20%";
                "data": "id",
                "render": function(data) {
                    return `<div class="text-center">
                            <a href="Books/Edit?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'> Edit</a>
                            <a class='btn btn-danger text-white' style='cursor:pointer;width:70px' onclick="Delete('api/DeleteBook',${data})"> Delete</a> </div>`;
                }
            }],
        "language": {
            "emptyTable":"No data found"
        },
        "width":"100%"
    });
}
function Delete(url, id) {
    swal({
        title: "Are you sure you want to delete",
        text: "Once deleted, record cannot be recorvered",
        icon: "warning",
        dangerMode: true,
    }).then(willDelete){
        if (willDelete) {
            $.ajax({
                url: url,
                data: {
                    "id": id
                },
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else
                    {
                        toastr.error(data.message);
                    }
                }
            });
        }
    }
}