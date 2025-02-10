import { SVG, extend } from 'https://cdn.jsdelivr.net/npm/@svgdotjs/svg.js@3.0/dist/svg.esm.js'

let draw;

export function createSvg(containerId, width, height) {
    debugger;
    const container = document.getElementById(containerId);
    container.innerHTML = ''; // Clear previous SVG, if any
    draw = SVG().addTo(container).size(width, height);
    draw.attr('id', 'mainSvg'); // Add an ID to the SVG element
    draw.viewbox(0, 0, width, height); // set viewbox
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

    element.on('mouseup', function (event) {
        isDragging = false;
        element.removeClass('selected');
        reportElementInfo(this, type); // Report element info on mouse up
    });

    element.on('mouseleave', function (event) {
        isDragging = false;
        element.removeClass('selected');
    });

    element.on('click', function (event) {
        reportElementInfo(this, type); // Report element info on click
    });


}

function reportElementInfo(element, type) {
    let x, y, width, height, r, x1, y1, x2, y2;

    if (type === 'circle') {
        x = element.cx();
        y = element.cy();
        r = element.attr('r');
        width = 0;
        height = 0;
        x1 = 0;
        y1 = 0;
        x2 = 0;
        y2 = 0;
    } else if (type === 'rect') {
        x = element.x();
        y = element.y();
        width = element.width();
        height = element.height();
        r = 0;
        x1 = 0;
        y1 = 0;
        x2 = 0;
        y2 = 0;
    }
    else if (type === 'image') {
        x = element.x();
        y = element.y();
        width = element.width();
        height = element.height();
        r = 0;
        x1 = 0;
        y1 = 0;
        x2 = 0;
        y2 = 0;
    }
    else if (type === 'line') {
        x = x1 = element.attr('x1');
        y = y1 = element.attr('y1');
        width = 0;
        height = 0;
        r = 0;
        x2 = element.attr('x2');
        y2 = element.attr('y2');
    }

    debugger;
    DotNet.invokeMethodAsync('Blazor.SvgDiagram', 'SelectElement', type, x, y, width, height, r, x1, y1, x2, y2);
}