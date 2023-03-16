function showMessage() {
  alert('This destination has been added to your list!');
}

function deleteMessage() {
  alert('As you wish. Destination deleted!');
}

function cancelTrip() {
  alert('Reservation canceled! You can now remove from your list.');
}

let num = document.getElementById('num');

function genNumber() {
  let chars = '0123456789';
  let numLength = 1;
  let num = '';

  for (let i = 0; i <= numLength; i++) {
    let randomNumber = Math.floor(Math.random() * chars.length);
    num += chars.substring(randomNumber, randomNumber + 1);
  }

  document.getElementById('num').innerHTML = num;
}
