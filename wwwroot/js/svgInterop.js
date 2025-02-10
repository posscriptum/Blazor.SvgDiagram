import { SVG } from 'https://cdn.jsdelivr.net/npm/@svgdotjs/svg.js@3.0/dist/svg.esm.js'

let draw;

export function createSvg(containerId, width, height) {
    debugger;
    const container = document.getElementById(containerId);
    container.innerHTML = ''; 
    draw = SVG().addTo(container).size(width, height);
    draw.attr('id', 'mainSvg'); 
    draw.viewbox(0, 0, width, height); 
    return draw;
}

export function drawGrid(width, height, gridSize) {
    if (!draw) return;

    for (let i = gridSize; i < width; i += gridSize) {
        draw.line(i, 0, i, height).stroke({ width: 0.5, color: '#eee' });
    }
    for (let i = gridSize; i < height; i += gridSize) {
        draw.line(0, i, width, i).stroke({ width: 0.5, color: '#eee' });
    }
}


export function addRectangle(x, y, width, height, color) {
    debugger;
    if (!draw) return;
    const rect = draw.rect(width, height).attr({ x: x, y: y, fill: color, 'stroke-width': 2, stroke: 'black' });
    makeDraggable(rect, 'rect');
}

export function addCircle(cx, cy, r, color) {
    debugger;
    if (!draw) return;
    const circle = draw.circle(2 * r).attr({ cx: cx, cy: cy, fill: color, 'stroke-width': 2, stroke: 'black' });
    makeDraggable(circle, 'circle');
}

export function addLine(x1, y1, x2, y2, color) {
    if (!draw) return;
    const line = draw.line(x1, y1, x2, y2).stroke({ color: color, width: 4, 'stroke-width': 2, stroke: 'black' });
    makeDraggable(line, 'line');
}


export function addImage(url, x, y, width, height) {
    if (!draw) return;
    const image = draw.image(url).attr({ x: x, y: y, width: width, height: height });
    makeDraggable(image, 'image');
}


function makeDraggable(element, type) {
    debugger;
    if (!draw) return;
    let isDragging = false;
    let startX;
    let startY;
    let initialX;
    let initialY;

    element.on('mousedown', function (event) {
        event.preventDefault();
        isDragging = true;
        startX = event.clientX;
        startY = event.clientY;

        if (type === 'circle') {
            initialX = this.cx();
            initialY = this.cy();
        }
        else if (type === 'rect' || type === 'image') {
            initialX = this.x();
            initialY = this.y();
        }

        element.addClass('selected');
    });

    element.on('mousemove', function (event) {
        if (isDragging) {
            const currentX = event.clientX;
            const currentY = event.clientY;
            const deltaX = currentX - startX;
            const deltaY = currentY - startY;

            if (type === 'circle') {
                this.center(initialX + deltaX, initialY + deltaY);
            } else if (type === 'rect' || type === 'image') {
                this.move(initialX + deltaX, initialY + deltaY);
            }
            else if (type === 'line') {
                this.attr('x1', this.attr('x1') + deltaX);
                this.attr('y1', this.attr('y1') + deltaY);
                this.attr('x2', this.attr('x2') + deltaX);
                this.attr('y2', this.attr('y2') + deltaY);
            }
        }
    });

    element.on('mouseup', function () {
        isDragging = false;
        element.removeClass('selected');
        reportElementInfo(this, type); 
    });

    element.on('mouseleave', function () {
        isDragging = false;
        element.removeClass('selected');
    });

    element.on('click', function () {
        reportElementInfo(this, type); 
    });
}

function reportElementInfo(element, type) {
    let elementInfo = {
        Type: type,
        X: 0,
        Y: 0,
        Width: 0,
        Height: 0,
        Radius: 0,
        X1: 0,
        Y1: 0,
        X2: 0,
        Y2: 0
    };

    switch (type) {
        case 'circle':
            elementInfo.X = element.cx();
            elementInfo.Y = element.cy();
            elementInfo.Radius = element.attr('r');
            break;
        case 'rect':
        case 'image':
            elementInfo.X = element.x();
            elementInfo.Y = element.y();
            elementInfo.Width = element.width();
            elementInfo.Height = element.height();
            break;
        case 'line':
            elementInfo.X = elementInfo.X1 = element.attr('x1');
            elementInfo.Y = elementInfo.Y1 = element.attr('y1');
            elementInfo.X2 = element.attr('x2');
            elementInfo.Y2 = element.attr('y2');
            break;
    }

    DotNet.invokeMethodAsync('Blazor.SvgDiagram', 'SelectElement', elementInfo);
}