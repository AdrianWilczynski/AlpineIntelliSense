# Alpine.js IntelliSense

Simple IntelliSense & Snippets for [Alpine.js](https://github.com/alpinejs/alpine) framework.

## IntelliSense

IntelliSense for Alpine's directives.

![Screenshot - Directives](img/screenshot-directives.png)

## Snippets

![Screenshot - Snippet](img/screenshot-snippet.png)

| Prefix | Body |
| ------ | ---- |
| x\-if | `<template x-if="${1:true}"> $0 </template>` |
| x\-for | `<template x-for="${1:item} in ${2:items}"> $0 </template>` |
| x\-for\-key | `<template x-for="${1:item} in ${2:items}" :key="${3:item}"> $0 </template>` |
| x\-bind:class | `x-bind:class="{ '${1:hidden}': ${2:foo} }"` |
| x\-bind | `x-bind:${1:attribute}="${2:expression}"` |
| $el | `$el` |
| $refs | `$refs.${1:name}` |
| $event | `$event` |
| $dispatch | `$dispatch('${1:custom-event}', ${2:{ foo: 'bar' }})` |
| $nextTick | `$nextTick(${1:() => { console.log($event.target.innerText) }});` |
| \.away | `.away` |
| \.prevent | `.prevent` |
| \.stop | `.stop` |
| \.window | `.window` |
| \.document | `.document` |
| \.once | `.once` |
| alpine\-cdn | `<script src="https://cdn.jsdelivr.net/gh/alpinejs/alpine@v${1:1.10.1}/dist/alpine.js" defer></script>` |