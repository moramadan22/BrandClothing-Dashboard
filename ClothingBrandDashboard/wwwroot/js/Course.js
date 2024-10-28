var dtable;
$(document).ready(function () {
    loadData();
    $('#customSearch').on('keyup', function () {
        if (dtable && typeof dtable.search === 'function') {
            dtable.search(this.value).draw(); // Filter the DataTable with the custom input
        } else {
            console.error("dtable is not defined or not a DataTable instance");
        }
    });
});


function loadData() {
    dtable = $("#myTable").DataTable({
        "ajax": {
            "url": "/Course/GetData"
        },
        "columns": [
            { "data": "title" },
            { "data": "description" },
            { "data": "duration" },
            { "data": "price" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="d-flex">
                      <a href="/Course/Edit/${data}" class="btn btn-warning" style="width: 100px; height: 40px; display: flex; justify-content: center; align-items: center; margin-right: 10px;">Edit</a>
                      <a onClick="x('/Course/Delete/${data}')" class="btn btn-danger" style="width: 100px; height: 40px; display: flex; justify-content: center; align-items: center;">Delete</a>
                    </div>

                    `
                }
            }
        ]
    });
}
function x(url) {
    //alert(url)
    //"/Admin/Product/Delete/${data}"
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            console.log("a1")
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    console.log(data)

                    if (data.success) {
                        //window.location.reload();

                        //dtable.ajax.reload();
                        //toaster.success(data.message);
                        //console.log(dtable.ajax.reload());
                        //window.location.reload();

                        //window.location.reload();

                    }
                    else { toaster.error(data.message); }
                    //dtable.ajax.reload();
                },

            });

            //window.location.reload();

            console.log("r1")

            swalWithBootstrapButtons.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
                
            });
            window.location.reload();

            //window.location.reload();
            //dtable.ajax.reload();

            console.log("r2")

        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire({
                title: "Cancelled",
                text: "Your imaginary file is safe :)",
                icon: "error"
            });
        }
    });
    console.log("r3")


}
