var dtable;
$(document).ready(function () {
    loadCategories(); 
    loadData();
    $('#customSearch').on('keyup', function () {
        dtable.search(this.value).draw(); // Filter the DataTable with the custom input
    });

    // Click event for the search icon
    //$('#searchIcon').on('click', function () {
    //    var searchValue = $('#customSearch').val();
    //    dtable.search(searchValue).draw(); // Filter the DataTable with the current input value
    //});

    $('#categoryDropdown').on('change', function () {
        const selectedValue = $(this).val(); // Get the selected value from the dropdown
        if (dtable && typeof dtable.column === 'function') {
            dtable.column(0).search(selectedValue).draw(); // Assuming category is in the first column (index 0)
        } else {
            console.error("dtable is not defined or not a DataTable instance");
        }
    });



});

function loadCategories() {
    $.ajax({
        url: "/Product/GetCategories", // Your API endpoint to get categories
        method: "GET",
        success: function (response) {
            const categories = response.categories; // Access the categories property
            if (Array.isArray(categories)) { // Ensure it's an array before iterating
                categories.forEach(function (category) {
                    console.log("in for");
                    // Assuming you have a select dropdown to filter by categories
                    $('#categoryDropdown').append(`<option value="${category.name}">${category.name}</option>`);
                });
            } else {
                console.error("Categories is not an array", categories);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching categories:", error);
        }
    });
}



function loadData() {
    dtable = $("#myTable").DataTable({ // Use DataTable instead of dataTable
        "ajax": {
            "url": "/Product/GetData"
        },
        "columns": [
            { "data": "categoryName" },
            { "data": "name" },
            { "data": "description" },
            { "data": "price" },
            { "data": "stockQuantity" },
            { "data": "discount" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="d-flex">
                      <a href="/Product/Edit/${data}" class="btn btn-warning" style="width: 100px; height: 40px; display: flex; justify-content: center; align-items: center; margin-right: 10px;">Edit</a>
                      <a onClick="x('/Product/Delete/${data}')" class="btn btn-danger" style="width: 100px; height: 40px; display: flex; justify-content: center; align-items: center;">Delete</a>
                    </div>

                    `
                }
            }
        ],
        //"searching": false // Disable the default search box
        //"dom": 'Bfrtip', // Position the buttons
        //"buttons": [
        //    {
        //        text: 'Add Product',
        //        className: 'btn btn-success',
        //        action: function (e, dt, node, config) {
        //            // Action to perform when button is clicked
        //            window.location.href = '/Product/Create'; // Redirect to create page
        //        }
        //    },
          
        //]
    
    });
}
// Toggle search input


function x(url) {
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
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                    //    toaster.success(data.message);
                    } else {
                        toaster.error(data.message);
                    }
                }
            });

            swalWithBootstrapButtons.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
            console.log("before")

            window.location.reload();
            //dtable.ajax.reload();

            console.log("after")
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire({
                title: "Cancelled",
                text: "Your imaginary file is safe :)",
                icon: "error"
            });
        }
    });
}
