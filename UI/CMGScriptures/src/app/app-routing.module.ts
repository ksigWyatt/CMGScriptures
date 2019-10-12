import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { EditorComponent } from './editor/editor.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [  
  {path: "", component: HomeComponent},
  {path: "editor", component: EditorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
