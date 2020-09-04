
$(document).ready(function () {
     $('.leftmenu .dropdown > .mv').css("color", "#F4691A");
        $('.pagetitle h1').each(function () {
            var text = $(this).text();
            $(this).text(text.replace('Dashboard', 'Approved Voucher'));
        });
        $('.breadcrumb li').each(function () {
            var text = $(this).text();
            $(this).text(text.replace('Dashboard', 'Approved Voucher'));
        });


        $('#vouchergrid').kendoGrid({
            dataSource: { Name: 'Maruf', Phone: '011', Name: 'Maruf', Phone: '011', Name: 'Maruf', Phone: '011', Name: 'Maruf', Phone: '011' },
            selectable: true,
            columns: [
                 {
                     field: "Name",
                     title: "Name",
                 },
                 {
                     field: "Phone",
                     title: "Phone",
                 }

            ],
            pageable: {
                refresh: true,
                pageSize: true,
                pageSizes: [5, 10, 25, 50, 100, "All"],
                messages: {
                    itemsPerPage: "Approved Vouchers",
                    display: "{0}-{1} from {2} Approved Vouchers",
                    empty: "No Voucher Found",
                    allPages: "Show All"
                }
            },
            detailTemplate: kendo.template($("#detail-template").html()),
            detailInit: detailInit,
            dataBound: function () {
                this.expandRow(this.tbody.find("tr.k-master-row"));
            },
        });
        
    });
