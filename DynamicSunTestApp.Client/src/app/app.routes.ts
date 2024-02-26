import { Routes } from '@angular/router';
import {MainComponent} from "./pages/main/main.component";
import {ArchivesComponent} from "./components/archives/archives.component";
import {UploadComponent} from "./components/upload/upload.component";
import {ArchiveRecordsComponent} from "./components/archive-records/archive-records.component";

export const routes: Routes = [
  {
    path: '', component: MainComponent,
    children: [
      { path: 'archives', component: ArchivesComponent },
      { path: 'archives/:id', component: ArchiveRecordsComponent },
      { path: 'upload', component: UploadComponent },
    ]
  }
];
