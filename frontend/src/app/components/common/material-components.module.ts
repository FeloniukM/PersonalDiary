import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';

@NgModule({
    imports: [
        CommonModule,
        MatInputModule,
        MatButtonModule,
        MatFormFieldModule
    ],
    exports: [
        MatInputModule,
        MatButtonModule,
        MatFormFieldModule
    ],
    declarations: []
})
export class MaterialComponentsModule {}
