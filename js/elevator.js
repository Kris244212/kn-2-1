// DOM references
const elevatorEl = document.getElementById('elevator');
const indicator = document.getElementById('indicator');
const statusFloor = document.getElementById('status-floor');
const statusState = document.getElementById('status-state');
// ВИПРАВЛЕНО: Використовуємо клас .buttons, як у HTML
const buttons = document.querySelectorAll('.buttons button'); 
const shaft = document.getElementById('shaft');

const FLOORS = 10; // 0..9
let currentFloor = 0;
let moving = false;
let direction = 'up';

let calls = []; // queue of requested floors

// helper to update status UI
function setStatus(floor, state){
  statusFloor.textContent = floor;
  statusState.textContent = state;
  indicator.textContent = floor;
}

// attach click handlers
buttons.forEach(btn=>{
  btn.addEventListener('click', ()=>{
    const f = Number(btn.dataset.floor);
    // Видалено візуальний press-ефект, оскільки CSS :active його реалізує
    addCall(f);
  });
});

// add call if not in queue
function addCall(floor){
  if (!calls.includes(floor) && floor !== currentFloor) {
    calls.push(floor);
    
    // UX: підсвічування кнопки виклику
    const b = document.querySelector(`.buttons button[data-floor='${floor}']`); 
    if(b){ 
        b.style.filter = 'brightness(0.95)'; 
        setTimeout(()=>b.style.filter='', 600);
    }
  }
  processQueue();
}

// main processor
function processQueue(){
  if (moving) return;
  if (calls.length === 0) return;
  // choose next based on current direction to minimize reversals
  const upCalls = calls.filter(c=>c>currentFloor).sort((a,b)=>a-b);
  const downCalls = calls.filter(c=>c<currentFloor).sort((a,b)=>b-a);

  let next = null;
  if(direction==='up' && upCalls.length) next = upCalls[0];
  else if(direction==='down' && downCalls.length) next = downCalls[0];
  else if(upCalls.length){ direction='up'; next = upCalls[0]; }
  else if(downCalls.length){ direction='down'; next = downCalls[0]; }

  if(next !== null) moveTo(next);
}

// compute bottom position and move
function moveTo(targetFloor){
  moving = true;
  setStatus(currentFloor,'moving');
  // compute heights
  const shaftHeight = shaft.clientHeight;
  const floorHeight = shaftHeight / FLOORS; // each floor height
  const bottomPx = targetFloor * floorHeight;

  // close doors before moving
  closeDoors();

  // animate cabin
  requestAnimationFrame(()=>{
    elevatorEl.style.bottom = bottomPx + 'px';
  });

  // estimated time to arrive (match CSS transition ~900ms)
  const travelTime = 950;
  setTimeout(()=>{
    currentFloor = targetFloor;
    // remove from queue
    calls = calls.filter(c=>c!==targetFloor);

    // open doors when arrived
    openDoors();
    setStatus(currentFloor,'idle');

    // wait while doors open (1400ms)
    setTimeout(()=>{
      closeDoors();
      moving=false;
      // continue processing next
      processQueue();
    }, 1400);

  }, travelTime);
}

function openDoors(){
  const l = elevatorEl.querySelector('.door.left');
  const r = elevatorEl.querySelector('.door.right');
  if(l && r){
    l.style.transform = 'translateX(-100%)';
    r.style.transform = 'translateX(100%)';
  }
}
function closeDoors(){
  const l = elevatorEl.querySelector('.door.left');
  const r = elevatorEl.querySelector('.door.right');
  if(l && r){
    l.style.transform = 'translateX(0)';
    r.style.transform = 'translateX(0)';
  }
}

// initial status
setStatus(0,'idle');

// optional: keyboard debug (1..9, 0)
document.addEventListener('keydown', (e)=>{
  if(e.key >= '0' && e.key <= '9'){
    addCall(Number(e.key));
  }
});