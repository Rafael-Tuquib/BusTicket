$(function () {
    let allFromBarangays = [];
    let allToBarangays = [];

    function resetBarangay(targetField) {
        const prefix = targetField === 'from' ? 'From' : 'To';
        $('#' + prefix + 'BarangaySearch')
            .val('')
            .prop('disabled', true)
            .attr('placeholder', 'Select municipality first...');
        $('#' + prefix + 'BarangayId').val('');
        $('#' + prefix + 'BarangayDropdown').empty().hide();

        if (targetField === 'from') {
            allFromBarangays = [];
        } else {
            allToBarangays = [];
        }
    }

    function loadBarangays(municipalityId, targetField, selectedBarangayId) {
        const prefix = targetField === 'from' ? 'From' : 'To';
        const $search = $('#' + prefix + 'BarangaySearch');
        const $barangayId = $('#' + prefix + 'BarangayId');

        if (!municipalityId) {
            resetBarangay(targetField);
            updateRouteEstimate();
            return;
        }

        $search
            .prop('disabled', true)
            .attr('placeholder', 'Loading barangays...')
            .val('');
        $barangayId.val('');

        $.get('/Location/GetBarangays', { municipalityId: municipalityId })
            .done(function (data) {
                if (targetField === 'from') {
                    allFromBarangays = data || [];
                    updateFromBarangayDropdown('');
                } else {
                    allToBarangays = data || [];
                    updateToBarangayDropdown('');
                }

                $search
                    .prop('disabled', false)
                    .attr('placeholder', 'Search barangay...');

                if (selectedBarangayId) {
                    const selected = (data || []).find(b => String(b.id) === String(selectedBarangayId));
                    if (selected) {
                        $search.val(selected.name);
                        $barangayId.val(selected.id);
                    }
                }

                updateRouteEstimate();
            })
            .fail(function () {
                resetBarangay(targetField);
                alert('Unable to load barangays.');
            });
    }

    function renderBarangayDropdown($dropdown, barangays, filter, onSelect) {
        $dropdown.empty();

        const filtered = barangays.filter(b =>
            (b.name || '').toLowerCase().includes((filter || '').toLowerCase())
        );

        if (filtered.length === 0) {
            $('<div>')
                .addClass('dropdown-item text-muted')
                .text('No barangays found')
                .css({ padding: '8px 12px' })
                .appendTo($dropdown);
            return;
        }

        filtered.forEach(b => {
            $('<div>')
                .addClass('dropdown-item')
                .text(b.name)
                .attr('data-id', b.id)
                .css({
                    padding: '8px 12px',
                    cursor: 'pointer',
                    borderBottom: '1px solid #f0f0f0'
                })
                .hover(
                    function () { $(this).css('background-color', '#f8f9fa'); },
                    function () { $(this).css('background-color', 'white'); }
                )
                .on('click', function () {
                    onSelect(b);
                    $dropdown.hide();
                    updateRouteEstimate();
                })
                .appendTo($dropdown);
        });
    }

    function updateFromBarangayDropdown(filter) {
        renderBarangayDropdown($('#FromBarangayDropdown'), allFromBarangays, filter, function (barangay) {
            $('#FromBarangaySearch').val(barangay.name);
            $('#FromBarangayId').val(barangay.id);
        });
    }

    function updateToBarangayDropdown(filter) {
        renderBarangayDropdown($('#ToBarangayDropdown'), allToBarangays, filter, function (barangay) {
            $('#ToBarangaySearch').val(barangay.name);
            $('#ToBarangayId').val(barangay.id);
        });
    }

    function updateRouteEstimate() {
        const fromBarangayId = $('#FromBarangayId').val();
        const toBarangayId = $('#ToBarangayId').val();

        if (!fromBarangayId || !toBarangayId) {
            $('#Distance').val('');
            $('#recommendedFare').text('-');
            $('#Fare').val('');
            return;
        }

        $.post('/Ticket/EstimateRoute', { fromBarangayId: fromBarangayId, toBarangayId: toBarangayId })
            .done(function (res) {
                $('#Distance').val(parseFloat(res.distance).toFixed(2));
                $('#recommendedFare').text('PHP ' + parseFloat(res.fare).toFixed(2));
                $('#Fare').val(parseFloat(res.fare).toFixed(2));
            })
            .fail(function (xhr) {
                const msg = xhr.responseJSON && xhr.responseJSON.error
                    ? xhr.responseJSON.error
                    : 'Unable to estimate route.';
                console.warn(msg);
            });
    }

    $('#FromMunicipalityId').on('change', function () {
        loadBarangays($(this).val(), 'from');
    });

    $('#ToMunicipalityId').on('change', function () {
        loadBarangays($(this).val(), 'to');
    });

    $('#FromBarangaySearch').on('input', function () {
        $('#FromBarangayId').val('');
        updateFromBarangayDropdown($(this).val());
        $('#FromBarangayDropdown').show();
        updateRouteEstimate();
    }).on('focus', function () {
        if (allFromBarangays.length > 0) {
            updateFromBarangayDropdown($(this).val());
            $('#FromBarangayDropdown').show();
        }
    });

    $('#ToBarangaySearch').on('input', function () {
        $('#ToBarangayId').val('');
        updateToBarangayDropdown($(this).val());
        $('#ToBarangayDropdown').show();
        updateRouteEstimate();
    }).on('focus', function () {
        if (allToBarangays.length > 0) {
            updateToBarangayDropdown($(this).val());
            $('#ToBarangayDropdown').show();
        }
    });

    $('#FromBarangayDropdownBtn').on('click', function () {
        if ($('#FromBarangaySearch').prop('disabled')) return;

        const $dropdown = $('#FromBarangayDropdown');
        if ($dropdown.is(':visible')) {
            $dropdown.hide();
        } else {
            updateFromBarangayDropdown('');
            $dropdown.show();
        }
    });

    $('#ToBarangayDropdownBtn').on('click', function () {
        if ($('#ToBarangaySearch').prop('disabled')) return;

        const $dropdown = $('#ToBarangayDropdown');
        if ($dropdown.is(':visible')) {
            $dropdown.hide();
        } else {
            updateToBarangayDropdown('');
            $dropdown.show();
        }
    });

    $(document).on('click', function (e) {
        if (!$(e.target).closest('#FromBarangaySearch, #FromBarangayDropdownBtn, #FromBarangayDropdown').length) {
            $('#FromBarangayDropdown').hide();
        }
        if (!$(e.target).closest('#ToBarangaySearch, #ToBarangayDropdownBtn, #ToBarangayDropdown').length) {
            $('#ToBarangayDropdown').hide();
        }
    });

    const initialFromMunicipalityId = $('#FromMunicipalityId').val();
    const initialToMunicipalityId = $('#ToMunicipalityId').val();
    const initialFromBarangayId = $('#FromBarangayId').val();
    const initialToBarangayId = $('#ToBarangayId').val();

    if (initialFromMunicipalityId) {
        loadBarangays(initialFromMunicipalityId, 'from', initialFromBarangayId);
    } else {
        resetBarangay('from');
    }

    if (initialToMunicipalityId) {
        loadBarangays(initialToMunicipalityId, 'to', initialToBarangayId);
    } else {
        resetBarangay('to');
    }
});
