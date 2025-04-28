// Create an empty array to store the items
let shoppingList = JSON.parse(localStorage.getItem('shoppingList')) || [];

// Function to add a items to the list
function addItem() {
    // Get the value from the input field
    let itemName = document.getElementById('item-input').value;
    let itemQuantity = document.getElementById('quantity-input').value.trim();

    if (itemName && itemQuantity) {
        // Add the item name and quantity to the array
        shoppingList.push({
            name: itemName,
            quantity: itemQuantity,
            checked: false
        });

        // Save updated list to localStorage
        saveList();

        // Call a function to update the display of the list
        displayItems();

        // Clear the input fields after adding
        document.getElementById('item-input').value = '';
        document.getElementById('quantity-input').value = '';
    } else {
        alert('Please enter both a name and quantity!');
    }
}

// Function to save the list to localStorage
function saveList() {
    localStorage.setItem('shoppingList', JSON.stringify(shoppingList));
}

// Function to remove an item
function removeItem(index) {
    // Remove the item from the array
    shoppingList.splice(index, 1);

    // Update localStorage
    saveList();

    // Update display
    displayItems();
}

// Function to toggle checkbox
function toggleChecked(index) {
    shoppingList[index].checked = !shoppingList[index].checked;
    saveList();
    displayItems();
}

// Function to display the list of items in the <ul>
function displayItems() {
    // Get the <ul> element where the list will be displayed
    let ul = document.getElementById('item-list');

    // Clear the current list
    ul.innerHTML = '';

    // Loop through the shoppingList array and create <li> elements
    shoppingList.forEach(function (item, index) {
        let li = document.createElement('li');

        // Create checkbox
        let checkbox = document.createElement('input');
        checkbox.type = 'checkbox';
        checkbox.style.marginRight = '10px';
        checkbox.checked = item.checked;

        // Toggle when checkbox changes
        checkbox.addEventListener('change', function () {
            toggleChecked(index);
        });

        // Create span for item text
        let span = document.createElement('span');
        span.textContent = `${item.name} (${item.quantity})`;

        // Create delete button
        let deleteBtn = document.createElement('button');
        deleteBtn.textContent = 'Remove';
        deleteBtn.style.marginLeft = '10px';
        deleteBtn.onclick = function () {
            removeItem(index);
        };

        // Add event when checkbox is clicked
        checkbox.addEventListener('change', function () {
            if (checkbox.checked) {
                span.style.textDecoration = 'line-through';
            } else {
                span.style.textDecoration = 'none';
            }
        });

        // Assemble the list item
        li.appendChild(checkbox);
        li.appendChild(span);
        li.appendChild(deleteBtn);

        ul.appendChild(li);
    });
}

function showListInAlert() {
    if (shoppingList.length === 0) {
        alert('Your shopping list is empty.');
        return;
    }

    let message = 'Shopping List:\n';
    shoppingList.forEach(item => {
        message += `- ${item.name} (${item.quantity})\n`;
    });

    alert(message);
}

function showListInToast() {
    if (shoppingList.length === 0) {
        createToast('Your shopping list is empty.');
        return;
    }

    let message = shoppingList.map(item => `${item.name} (${item.quantity})`).join(', ');
    createToast('Shopping List: ' + message);
}

function createToast(message) {
    let toastContainer = document.getElementById('toast-container');

    let toast = document.createElement('div');
    toast.className = 'toast';
    toast.textContent = message;

    toastContainer.appendChild(toast);

    // Automatically remove the toast after 5 seconds
    setTimeout(function () {
        toast.style.opacity = '0';
        setTimeout(function () {
            toast.remove();
        }, 500); // Matches the CSS transition
    }, 5000);
}

displayItems();