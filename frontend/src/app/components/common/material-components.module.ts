import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
    imports: [
        CommonModule,
        MatInputModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule
    ],
    exports: [
        MatInputModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule
    ],
    declarations: []
})
export class MaterialComponentsModule {}
