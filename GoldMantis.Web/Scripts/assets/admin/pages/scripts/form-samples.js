var FormSamples = function () {


    return {
        //main function to initiate the module
        init: function () {
            alert(1);
            // use select2 dropdown instead of chosen as select2 works fine with bootstrap on responsive layouts.
            $('.select2_category').select2({
	            placeholder: "Select an option",
	            allowClear: true
	        });

        }

    };

}();