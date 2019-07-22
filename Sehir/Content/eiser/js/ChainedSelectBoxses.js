var $c_code = $('#c_code'),
    $c_name = $('#c_name'),
    $hTitle = $("#hTitle");
$options = $c_name.find('option');
$options1 = $hTitle.find('option');

$c_code.on('change', function () {
    $c_name.html($options.filter('[value="' + this.value + '"]'));
    $hTitle.html($options1.filter('[data-options="' + this.value + '"]'))
}).trigger('change');  