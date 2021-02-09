const events={abort:"abort",afterprint:"afterprint",animationend:"animationend",animationiteration:"animationiteration",animationstart:"animationstart",beforeprint:"beforeprint",beforeunload:"beforeunload",blur:"blur",canplay:"canplay",canplaythrough:"canplaythrough",change:"change",click:"click",contextmenu:"contextmenu",copy:"copy",cut:"cut",dblclick:"dblclick",drag:"drag",dragend:"dragend",dragenter:"dragenter",dragleave:"dragleave",dragover:"dragover",dragstart:"dragstart",drop:"drop",durationchange:"durationchange",ended:"ended",error:"error",focus:"focus",focusin:"focusin",focusout:"focusout",fullscreenchange:"fullscreenchange",fullscreenerror:"fullscreenerror",hashchange:"hashchange",input:"input",invalid:"invalid",keydown:"keydown",keypress:"keypress",keyup:"keyup",load:"load",loadeddata:"loadeddata",loadedmetadata:"loadedmetadata",loadstart:"loadstart",message:"message",mousedown:"mousedown",mouseenter:"mouseenter",mouseleave:"mouseleave",mousemove:"mousemove",mouseover:"mouseover",mouseout:"mouseout",mouseup:"mouseup",mousewheel:"mousewheel",offline:"offline",online:"online",open:"open",pagehide:"pagehide",pageshow:"pageshow",paste:"paste",pause:"pause",play:"play",playing:"playing",popstate:"popstate",progress:"progress",ratechange:"ratechange",resize:"resize",reset:"reset",scroll:"scroll",search:"search",seeked:"seeked",seeking:"seeking",select:"select",show:"show",stalled:"stalled",storage:"storage",submit:"submit",suspend:"suspend",timeupdate:"timeupdate",toggle:"toggle",touchcancel:"touchcancel",touchend:"touchend",touchmove:"touchmove",touchstart:"touchstart",transitionend:"transitionend",unload:"unload",volumechange:"volumechange",waiting:"waiting",wheel:"wheel"};
const KeyCodes={BACKSPACE:8,TAB:9,ENTER:13,SHIFT:16,CTRL:17,ALT:18,PAUSE:19,CAPS_LOCK:20,ESCAPE:27,SPACE:32,PAGE_UP:33,PAGE_DOWN:34,END:35,HOME:36,ARROW_LEFT:37,ARROW_UP:38,ARROW_RIGHT:39,ARROW_DOWN:40,INSERT:45,DELETE:46,KEY_0:48,KEY_1:49,KEY_2:50,KEY_3:51,KEY_4:52,KEY_5:53,KEY_6:54,KEY_7:55,KEY_8:56,KEY_9:57,KEY_A:65,KEY_B:66,KEY_C:67,KEY_D:68,KEY_E:69,KEY_F:70,KEY_G:71,KEY_H:72,KEY_I:73,KEY_J:74,KEY_K:75,KEY_L:76,KEY_M:77,KEY_N:78,KEY_O:79,KEY_P:80,KEY_Q:81,KEY_R:82,KEY_S:83,KEY_T:84,KEY_U:85,KEY_V:86,KEY_W:87,KEY_X:88,KEY_Y:89,KEY_Z:90,LEFT_META:91,RIGHT_META:92,SELECT:93,NUMPAD_0:96,NUMPAD_1:97,NUMPAD_2:98,NUMPAD_3:99,NUMPAD_4:100,NUMPAD_5:101,NUMPAD_6:102,NUMPAD_7:103,NUMPAD_8:104,NUMPAD_9:105,MULTIPLY:106,ADD:107,SUBTRACT:109,DECIMAL:110,DIVIDE:111,F1:112,F2:113,F3:114,F4:115,F5:116,F6:117,F7:118,F8:119,F9:120,F10:121,F11:122,F12:123,NUM_LOCK:144,SCROLL_LOCK:145,SEMICOLON:186,EQUALS:187,COMMA:188,DASH:189,PERIOD:190,FORWARD_SLASH:191,GRAVE_ACCENT:192,OPEN_BRACKET:219,BACK_SLASH:220,CLOSE_BRACKET:221,SINGLE_QUOTE:222};
class TableHelper{
    static clearRows(table) {
        while(table.childElementCount > 1){
            table.removeChild(table.lastChild);
        }
    }
    static createRow(obj, keys){
        const tr = document.createElement('tr');
        keys.forEach(key => {
            const td = document.createElement('td');
            td.innerText = obj[key];
            tr.appendChild(td);
        });
         return tr;
    }
    static createHeader(table, keys){
        const tableHead = document.createElement('tr');
        table.appendChild(tableHead);
        keys.forEach(key => {
           const th = document.createElement('th');
           th.innerText = key.replace('_', ' '); // sneaky
           tableHead.appendChild(th);
        });
        return table;
    }
}
class Cache
{
    constructor(){
        this.STOARGE_SUPPORTED = typeof(Storage) !== "undefined";
        if(!this.STOARGE_SUPPORTED){
            this._cache = {};
        }
    }
    add(key,data) {
        if(this.STOARGE_SUPPORTED){
            localStorage.setItem(key, JSON.stringify(data));
        } else {
            this._cache[key] = data;
        }
    }
    get(key) {
        if(this.STOARGE_SUPPORTED){
            const item = localStorage.getItem(key);
            return item !== null ? JSON.parse(item) : null;
        }
        return this._cache[key] !== undefined ? this.cache[key] : null ;
    }
}