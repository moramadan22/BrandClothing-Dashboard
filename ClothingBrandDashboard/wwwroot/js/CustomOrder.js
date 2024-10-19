let dtable;
$(document).ready(function () {
    loadData();

});
 
function loadData() {
    dtable = $("#myTable").dataTable({
        "ajax": {
            "url": "/CustomOrder/GetData"
        },
        "columns": [
            { "data": "id" },
            { "data": "modelLength" },
            { "data": "designDescription" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <a href="/CustomOrder/Detail/${data}" class="btn btn-warning" >Detail</a>
                    <a onClick= x("/CustomOrder/Delete/${data}") class="btn btn-danger">Delete</a> 
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
                        dtable.ajax.reload();
                        toaster.success(data.message);
                        //window.location.reload();

                    }
                    else { toaster.error(data.message); }
                    dtable.ajax.reload();
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

//function DeleteItem(url) {
//    console.log(url)

//    const swalWithBootstrapButtons = Swal.mixin({
//        customClass: {
//            confirmButton: "btn btn-success",
//            cancelButton: "btn btn-danger"
//        },
//        buttonsStyling: false
//    });
//    swalWithBootstrapButtons.fire({
//        title: "Are you sure?",
//        text: "You won't be able to revert this!",
//        icon: "warning",
//        showCancelButton: true,
//        confirmButtonText: "Yes, delete it!",
//        cancelButtonText: "No, cancel!",
//        reverseButtons: true
//    }).then((result) => {
//        if (result.isConfirmed) {
//            //$.ajax({
//            //    url: url,
//            //    type: "POST", // Use POST instead
//            //    data: { _method: 'DELETE' },
//            //    success: function (data) {
//            //        if (data.success) {
//            //            dtable.ajax.reload();
//            //            toaster.success(data.message);
//            //        }
//            //        else { toaster.error(data.message); }
//            //    },

//            //});


//            swalWithBootstrapButtons.fire({
//                title: "Deleted!",
//                text: "Your file has been deleted.",
//                icon: "success"
//            });
//        }
//        else if (
//            /* Read more about handling dismissals below */
//            result.dismiss === Swal.DismissReason.cancel
//        ) {
//            swalWithBootstrapButtons.fire({
//                title: "Cancelled",
//                text: "Your imaginary file is safe :)",
//                icon: "error"
//            });
//        }
//    });

//}